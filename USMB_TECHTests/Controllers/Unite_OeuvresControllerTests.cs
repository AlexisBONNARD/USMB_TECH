using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Unite_OeuvresControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Unite_OeuvresController _controller;
        private Unite_OeuvreManager _manager;

        private Unite_Oeuvre _unite1;
        private Unite_Oeuvre _unite2;
        private Unite_Oeuvre _unite3;

        [TestInitialize]
        public void Initialize()
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
               .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
               .Options;
            _context = new UsmbTechDbContext(option);
            _manager = new Unite_OeuvreManager(_context);
            _controller = new Unite_OeuvresController(_manager);
            _unite1 = new Unite_Oeuvre { Nom_Unite_Oeuvre = "Unite1" };
            _unite2 = new Unite_Oeuvre { Nom_Unite_Oeuvre = "Unite2" };
            _unite3 = new Unite_Oeuvre { Nom_Unite_Oeuvre = "Unite3" };
            _context.Unite_Oeuvres.AddRange(_unite1, _unite2, _unite3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllUnite_Return_Ok() 
        {
            var action = await _controller.GetUnite_Oeuvres();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Unite_Oeuvre>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Unite_Oeuvre>), "l'unité d'oeuvre retourné est incorrect");
            Assert.AreEqual(3, returnedList.Count(), "le nombre d'éléments retournés est incorrect");
            Assert.IsTrue(returnedList.Any(u => u.Nom_Unite_Oeuvre == "Unite1"), "Unite1 est manquant");
            CollectionAssert.AreEquivalent(
                _context.Unite_Oeuvres.Select(u => u.Nom_Unite_Oeuvre).ToList(), 
                returnedList.Select(u => u.Nom_Unite_Oeuvre).ToList(), "les unités d'oeuvre retournées sont incorrectes");
        }

        [TestMethod]
        public async Task GetUniteById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetUnite_Oeuvre(_unite1.Id_Unite_Oeuvre);
            var okResult = action.Result as OkObjectResult;
            var returnedType = okResult.Value as Unite_Oeuvre;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsNotNull(returnedType, "Le type de prestation retourné est null");
            Assert.AreEqual(_unite1.Nom_Unite_Oeuvre, returnedType.Nom_Unite_Oeuvre, "L'unite d'oeuvre retourné est incorrect");
        }

        [TestMethod]
        public async Task GetUniteById_NonExistingId_Returns_NotFound()
        {
            var action = await _controller.GetUnite_Oeuvre(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "La réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Une unité a été trouvé");
        }

        [TestMethod]
        public async Task DeleteUnite_Return_NoContent()
        {
            var action = _controller.DeleteUnite_Oeuvre(_unite3.Id_Unite_Oeuvre);

            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas de type NotContentResult");
            Assert.IsNull(_context.Unite_Oeuvres.Find(_unite3.Id_Unite_Oeuvre), "L'unite d'oeuvre n'a pas été supprimé");
        }

        [TestMethod]
        public async Task DeleteUnite_ReturnNotFound()
        {
            var action = _controller.DeleteUnite_Oeuvre(5);

            Assert.IsNull(_context.Unite_Oeuvres.Find(5), "la réponse n'est pas nulle");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
        }

        [TestMethod]
        public async Task PostUnite_Return_CreatedAtAction()
        {
            var action = await _controller.PostUnite_Oeuvre(new Unite_Oeuvre { Nom_Unite_Oeuvre = "Unite4" });

            var foundType = _context.Unite_Oeuvres.FirstOrDefault(t => t.Nom_Unite_Oeuvre == "Unite4");

            Assert.IsNotNull(foundType, "L'unité n'a pas été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsInstanceOfType(foundType, typeof(Unite_Oeuvre), "L'unité trouvé n'est pas une Unité d'oeuvre");
        }

        [TestMethod]
        public async Task PostUnite_InvalidModelState_ReturnsBadRequest()
        {
            var unite = new Unite_Oeuvre
            {
                Id_Unite_Oeuvre = -1,
                Nom_Unite_Oeuvre = "Unite0"
            };
            _controller.ModelState.AddModelError("IUnite_Oeuvre", "ID invalide");
            var action = await _controller.PostUnite_Oeuvre(unite);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "La réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "La réponse n'est pas un BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutUnite_ValidUpdate_ReturnsNoContent()
        {
            var updatedUnit = new Unite_Oeuvre
            {
                Id_Unite_Oeuvre = _unite1.Id_Unite_Oeuvre,
                Nom_Unite_Oeuvre = "Unite1Updated"
            };
            var action = await _controller.PutUnite_Oeuvre(_unite1.Id_Unite_Oeuvre, updatedUnit);
            var unitInDb = _context.Unite_Oeuvres.Find(_unite1.Id_Unite_Oeuvre);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "La réponse n'est pas NoContentResult");
            Assert.AreEqual(updatedUnit.Nom_Unite_Oeuvre, unitInDb.Nom_Unite_Oeuvre, "L'unité n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutUnite_InvalidId_ReturnsBadRequest()
        {
            var updatedUnit = new Unite_Oeuvre
            {
                Id_Unite_Oeuvre = _unite1.Id_Unite_Oeuvre,
                Nom_Unite_Oeuvre = "Unite1Updated"
            };
            var action = await _controller.PutUnite_Oeuvre(8, updatedUnit);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "La réponse n'est pas BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutUnite_NonExistingId_ReturnsNotFound()
        {
            var updatedUnit = new Unite_Oeuvre
            {
                Id_Unite_Oeuvre = 10,
                Nom_Unite_Oeuvre = "Type10"
            };
            var action = await _controller.PutUnite_Oeuvre(10, updatedUnit);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "La réponse n'est pas NotFoundObjectResult");
        }
    }
}
