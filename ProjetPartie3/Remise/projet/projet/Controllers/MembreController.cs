using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Security.Authorization;
using ProjetISDP1.DataAccessLayer;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembreController : ControllerBase
    {
        DAL dal;

        public MembreController()
        {
            dal = new DAL();
        }

        // POST: api/<MembreController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorize(Roles.Admin)]
        public IActionResult Post([FromBody] Membre membre)
        {
            if (membre is null) return StatusCode(StatusCodes.Status412PreconditionFailed, "Membre invalide.");

            membre.Id = 0;

            try
            {
                dal.MembreFactory.Save(membre);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }
    }
}
