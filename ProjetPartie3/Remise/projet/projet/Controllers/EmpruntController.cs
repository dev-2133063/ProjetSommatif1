using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Security.Authorization;
using ProjetISDP1.DataAccessLayer;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Roles.Admin)]
    public class EmpruntController : ControllerBase
    {
        DAL dal;

        public EmpruntController()
        {
            dal = new DAL();
        }

        // POST: api/<EmpruntController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Emprunt emprunt)
        {
            if (emprunt is null) return StatusCode(StatusCodes.Status412PreconditionFailed, "Emprunt invalide.");

            emprunt.Id = 0;

            try
            {
                dal.EmpruntFactory.Save(emprunt);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }

        // DELETE api/<EmpruntController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest("Id invalide pour la suppression");
            if (dal.EmpruntFactory.Get(id) is null) return NotFound("Cet emprunt n'existe pas.");

            try
            {
                dal.EmpruntFactory.Delete(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }
    }
}
