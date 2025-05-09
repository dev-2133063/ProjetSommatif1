using projet.Models;
using projet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Tests.Helpers;
using ProjetISDP1.DataAccessLayer;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class TestAuteur
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
        [Description("Test de la méthode permettant la modification d'un auteur existant.")]
        public void ModificationAuteur()
        {
            DAL dal = new DAL();
            AuteurController controller = new AuteurController();

            int auteurId = 1;
            Auteur auteurModif = dal.AuteurFactory.Get(auteurId);

            //modif
            auteurModif.Nom = "Nom Modifié";
            IActionResult response = controller.Put(auteurModif.Id, auteurModif);
            Assert.IsInstanceOfType(response, typeof(OkResult));
            dal.AuteurFactory.Get(auteurId).Should().BeEquivalentTo(auteurModif);
            dal.AuteurFactory.Get(auteurId).Nom.Should().Be(auteurModif.Nom);

            response = controller.Put(54, auteurModif);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }


        [TestMethod]
        [Description("Test de la méthode qui ajoute un nouvel auteur.")]
        public void AjoutAuteur()
        {
            DAL dal = new DAL();
            AuteurController controller = new AuteurController();

            Auteur auteur = new Auteur(0, "TEST AJOUT");

            Assert.AreEqual(5, dal.AuteurFactory.GetAll().ToList().Count);
            IActionResult response = controller.Post(auteur);
            Assert.AreEqual(6, dal.AuteurFactory.GetAll().ToList().Count);

            //verifications
            Assert.IsInstanceOfType(response, typeof(OkResult));
            dal.AuteurFactory.GetAll().ToList().Last().Nom.Should().Be(auteur.Nom);
        }
    }
}