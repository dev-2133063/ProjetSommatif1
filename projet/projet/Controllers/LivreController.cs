using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Misc;
using projet.Models;
using projet.Security.Authorization;
using ProjetISDP1.DataAccessLayer;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Roles.User)]
    public class LivreController : ControllerBase
    {
        private DAL dal;
        public LivreController()
        {
            dal = new DAL();
        }

        // GET: api/<LivreController>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Livre>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetAll()
        {
            List<Livre> livres = dal.LivreFactory.GetAll().ToList();

            if (!livres.Any()) return NotFound("Aucun livre trouvé");

            return Ok(livres);
        }

        // GET: api/<LivreController>/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType<Livre>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<Livre> GetById(int id)
        {
            if (id < 0) return BadRequest("ID invalide.");
            Livre livre = dal.LivreFactory.Get(id);

            if (livre is null) return NotFound("Aucun livre trouvé avec ce ID");

            return Ok(livre);
        }

        // GET: api/<LivreController>/auteur/2
        [HttpGet("/auteur/{auteurId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetByAuteur(int auteurId)
        {
            if (auteurId < 0 || dal.AuteurFactory.Get(auteurId) is null) return BadRequest("ID invalide.");

            Livre[] livres = dal.LivreFactory.GetAll();

            if (livres is null || !livres.Any()) return NotFound("Aucun livre dans la banque de données.");

            List<Livre> livreCorrespondant = new List<Livre>();

            foreach (Livre livre in livres)
                if (livre.AuteurId == auteurId)
                    livreCorrespondant.Add(livre);

            if (livreCorrespondant.Any()) return Ok(livreCorrespondant);

            return NotFound("Aucun livre trouvés à partir de cet auteur.");
        }

        // GET: api/<LivreController>/category/2
        [HttpGet("/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetByCat(int categoryId)
        {
            if (categoryId < 0 || dal.CategoryFactory.Get(categoryId) is null) return BadRequest("ID invalide.");

            Livre[] livres = dal.LivreFactory.GetAll();

            if (livres is null || !livres.Any()) return NotFound("Aucun livre dans la banque de données.");

            List<Livre> livreCorrespondant = new List<Livre>();

            foreach (Livre livre in livres)
                if (livre.CategorieId == categoryId)
                    livreCorrespondant.Add(livre);

            if (livreCorrespondant.Any()) return Ok(livreCorrespondant);

            //else
            return NotFound("Aucun livre trouvés dans cette catégorie.");
        }


        // GET: api/<LivreController>/dispo
        [HttpGet("/dispo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetDispo()
        {
            Livre[] livres = dal.LivreFactory.GetDispo();

            if (livres.Any()) return Ok(livres);

            return NotFound("Aucun livre disponible.");
        }


        // GET: api/<LivreController>/indispo
        [HttpGet("/indispo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetInDispo()
        {
            Livre[] livres = dal.LivreFactory.GetDispo(false);

            if (livres.Any()) return Ok(livres);

            return NotFound("Aucun livre emprunté.");
        }

        // GET: api/<LivreController>/emprunte/2
        [HttpGet("/emprunte/{membreId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetInDispoMembId(int membreId)
        {
            if (membreId < 0) return BadRequest("ID invalide.");

            Livre[] livres = dal.LivreFactory.GetInDispoMembre(membreId);

            if (livres.Any()) return Ok(livres);

            return NotFound("Aucun livre correspondant à ce membre trouvé.");
        }

        // GET: api/<LivreController>/emprunt
        [HttpGet("/historiqueEmprunt/{membreId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType<List<Livre>>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<List<Livre>> GetHistEmprunt(int membreId)
        {
            if (membreId < 0) return BadRequest("ID invalide.");

            Livre[] livres = dal.LivreFactory.GetAllDejaEmprunte(membreId);

            if (livres.Any()) return Ok(livres);

            return NotFound("Aucun livre correspondant trouvé.");
        }

        // POST: api/<LivreController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorize(Roles.Admin)]
        public IActionResult Post([FromBody] Livre livre)
        {
            if (livre is null) return StatusCode(StatusCodes.Status412PreconditionFailed, "Livre invalide.");

            livre.Id = 0;

            try 
            { 
                dal.LivreFactory.Save(livre); 
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }

        // PUT: api/<LivreController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorize(Roles.Editor)]
        public IActionResult Put(int id, [FromBody] Livre livre)
        {
            if (id < 0 || livre is null) return StatusCode(StatusCodes.Status412PreconditionFailed);
            if (dal.LivreFactory.Get(id) is null) return NotFound();

            livre.Id = id;

            try 
            { 
                dal.LivreFactory.Save(livre); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }

        // DELETE api/<LivreController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorize(Roles.Admin)]
        public IActionResult Delete(int id)
        {
            if (dal.LivreFactory.Get(id) is null) return NotFound("Ce livre n'existe pas.");

            Livre[] livres = dal.LivreFactory.GetAllDejaEmprunte();

            foreach (Livre livre in livres)
                if (livre.Id == id)
                    return BadRequest("Impossible de supprimer ce livre, car il a une historique d'emprunt.");
            try
            {
                dal.LivreFactory.Delete(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur c'est produite lors du traitement de cette requete.");
            }

            return Ok();
        }
    }
}
