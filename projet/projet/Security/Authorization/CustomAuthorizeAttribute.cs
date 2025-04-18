﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ProjetISDP1.DataAccessLayer;

namespace projet.Security.Authorization
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private List<string> _roles;

        public CustomAuthorizeAttribute(params string[] Roles)
        {
            _roles = new List<string>(Roles);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();
            if (hasAllowAnonymous) { return; }

            CustomAuthorizeAttribute? apiKeyAttribute = context.ActionDescriptor.EndpointMetadata.OfType<CustomAuthorizeAttribute>().LastOrDefault()
                ?? throw new Exception("Ne devrait pas etre null");

            _roles = apiKeyAttribute._roles;

            string apiKey = context.HttpContext.Request.Headers["X-API-Key"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Il faut fournir la cle d'API."
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            DAL dal = new DAL();
            List<string> roles = dal.RolesFactory.GetRolesFromApiKey(apiKey);

            if (roles == null || !_roles.Intersect(roles).Any())
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    MessageProcessingHandler = "votre cle d'api be vous permet pas d'acceder a cette ressource"
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
                return;
            }
        }
    }
}
