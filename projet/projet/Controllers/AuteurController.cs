using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetISDP1.DataAccessLayer;
using projet.Models;
using System.Reflection.Metadata.Ecma335;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteurController : ControllerBase
    {
        private DAL dal;
        public AuteurController()
        {
            dal = new DAL();
        }

        // GET: api/<auteurController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Auteur>>(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Auteur>> Get()
        {
            Auteur[] auteurs = dal.AuteurFactory.GetAll();

            if (auteurs == null) return NotFound();
            else if (auteurs.Length == 0) return NoContent();

            return Ok(auteurs);
        }

        // GET: api/<auteurController>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Auteur>(StatusCodes.Status200OK)]
        public ActionResult<Auteur> Get(int id)
        {
            Auteur auteur = dal.AuteurFactory.Get(id);

            if (auteur is null)
            {
                return NotFound("Aucun auteur trouvé avec ce ID");
            }

            return Ok(auteur);
        }

        // POST: api/<AuteurController>/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult Post([FromBody]Auteur auteur)
        {
            if (auteur is null) return StatusCode(StatusCodes.Status412PreconditionFailed);

            auteur.Id = 0;

            try { dal.AuteurFactory.Save(auteur); }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }


        // PUT: api/<AuteurController>/5
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult Put(int id, [FromBody] Auteur auteur)
        {
            if (id < 0 || auteur is null) return StatusCode(StatusCodes.Status412PreconditionFailed);
            if (dal.AuteurFactory.Get(id) is null) return NotFound();

            auteur.Id = id;

            try { dal.AuteurFactory.Save(auteur); }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }

        // DELETE api/<FruitController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dal.AuteurFactory.Get(id) is null) return NotFound();
            foreach (Livre livre in dal.LivreFactory.GetAll())
                if (livre.Id == id) return StatusCode(StatusCodes.Status403Forbidden, "Cet auteur est associe a un livre, impossible de supprimer.");

            try { dal.AuteurFactory.Delete(id); }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }

    }
}
