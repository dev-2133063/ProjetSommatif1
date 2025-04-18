using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace projet.Security.Authentification
{
    public class ApiKeyAuthHandler : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "x-api-key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("CHECKING");

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Il faut fournir la clé d'API."
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apikey = configuration.GetValue<string>(ApiKeyHeaderName) ?? "";

            if (!apikey.Equals(potentialApiKey))
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Votre clé d'api ne vous permet pas d'accèder à cette ressource"
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            QuotaProcessor qp = new QuotaProcessor();

            Console.WriteLine("");
            if (!qp.LogQuota(apikey))
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Vous avez atteint votre limite d'appel pour la journée"
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            await next();
        }
    }
}
