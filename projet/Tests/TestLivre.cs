using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;
using projet.Models;
using MySqlX.XDevAPI;
using projet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Azure;
using Tests.Helpers;
using ProjetISDP1.DataAccessLayer;
using FluentAssertions;


namespace Tests
{
    [TestClass]
    public class TestLivre
    {
        [TestInitialize]
        public void Initialize()
        {
            new DatabaseHelper().CreateAndFillTestTables();
        }
        [TestCleanup]
        public void Cleanup()
        {
            new DatabaseHelper().DropTestTables();
        }

        [TestMethod]
        [Description("Test de la méthode qui récupère la listes des livres empruntés par un membre.")]
        public void GetLivreEmpruntesParMembre()
        {
            ActionResult<List<Livre>> response = new LivreController().GetInDispoMembId(-1);
            Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));

            response = new LivreController().GetInDispoMembId(1);
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));

            var nfResult = response.Result as NotFoundObjectResult;
            List<Livre> livres = response.Value;

            Assert.IsNull(livres);
        }

        [TestMethod]
        [Description("Test de la méthode supprime un livre existant s'il n'a jamais été emprunté.")]
        public void SuppressionLivreJamaisEmpruntes()
        {
            DAL dal = new DAL();
            LivreController controller = new LivreController();

            Assert.IsInstanceOfType(controller.Delete(0), typeof(NotFoundObjectResult));

            dal.LivreFactory.GetAll().ToList().Count.Should().Be(5);
            Assert.IsInstanceOfType(controller.Delete(1), typeof(BadRequestObjectResult));
            dal.LivreFactory.GetAll().ToList().Count.Should().Be(5);

            IActionResult response = controller.Delete(2);
            dal.LivreFactory.GetAll().ToList().Count.Should().Be(4);

            //Livre 2 n'à jamais été emprunté
            Assert.IsInstanceOfType(response, typeof(OkResult));
            foreach (Livre livre in dal.LivreFactory.GetAll())
                Assert.AreNotSame(2, livre.Id);
        }
    }
}