using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using projet.Controllers;
using projet.Models;
using ProjetISDP1.DataAccessLayer;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class TestEmprunt
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
        [Description("Test de la méthode qui permet l'ajout d'un nouvel emprunt.")]
        public void AjoutEmprunt()
        {
            DAL dal = new DAL();
            EmpruntController controller = new EmpruntController();

            Emprunt emprunt = new Emprunt(2, DateTime.Now, DateTime.Now.AddDays(7), 1, 1);
            dal.EmpruntFactory.GetAll().ToList().Count.Should().Be(1);
            IActionResult response = controller.Post(emprunt);
            dal.EmpruntFactory.GetAll().ToList().Count.Should().Be(2);
            //verifications
            Assert.IsInstanceOfType(response, typeof(OkResult));
            dal.EmpruntFactory.GetAll().ToList().Last().LivreId.Should().Be(emprunt.LivreId);
            dal.EmpruntFactory.GetAll().ToList().Last().MembreId.Should().Be(emprunt.MembreId);
        }

        [TestMethod]
        [Description("Test de la méthode qui supprime un emprunt existant.")]
        public void SuppressionEmprunt()
        {
            DAL dal = new DAL();
            EmpruntController controller = new EmpruntController();

            int empruntId = 1;

            dal.EmpruntFactory.GetAll().ToList().Count.Should().Be(1);
            IActionResult response = controller.Delete(empruntId);
            dal.EmpruntFactory.GetAll().ToList().Count.Should().Be(0);

            //Verification
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}