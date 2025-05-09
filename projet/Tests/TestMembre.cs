using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using projet.Controllers;
using projet.Models;
using ProjetISDP1.DataAccessLayer;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class TestMembre
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
        [Description("Test de la méthode d'ajout d'un membre.")]
        public void AjoutMembre()
        {
            DAL dal = new DAL();
            MembreController controller = new MembreController();
            Membre newMembre = new Membre(10, "TestAJoutMembre", "test@courriel.com", "123-432-6546", DateTime.Now);

            dal.MembreFactory.GetAll().ToList().Count.Should().Be(3);

            IActionResult response = controller.Post(newMembre);

            Assert.IsInstanceOfType(response, typeof(OkResult));

            dal.MembreFactory.GetAll().ToList().Count.Should().Be(4);
            dal.MembreFactory.GetAll().ToList().Last().Nom.Should().Be(newMembre.Nom);
            dal.MembreFactory.GetAll().ToList().Last().Courriel.Should().Be(newMembre.Courriel);
            dal.MembreFactory.GetAll().ToList().Last().Telephone.Should().Be(newMembre.Telephone);
        }
    }
}