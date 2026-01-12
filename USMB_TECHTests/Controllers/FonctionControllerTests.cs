using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class FonctionControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private FonctionController _controller;
        private FonctionManager _manager;

        private Fonction _fonction1;
        private Fonction _fonction2;
        private Fonction _fonction3;

        [TestInitialize]
        public void Initialize() 
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
               .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
               .Options;
            _context = new UsmbTechDbContext(option);
            _manager = new FonctionManager(_context);
            _controller = new FonctionController(_manager);
            _fonction1 = new Fonction { Nom_Fonction = "Fonction1" };
            _fonction2 = new Fonction { Nom_Fonction = "Fonction2" };
            _fonction3 = new Fonction { Nom_Fonction = "Fonction3" };
            _context.Fonctions.AddRange(_fonction1, _fonction2, _fonction3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllFonction_Return_Ok() 
        {
            var action = await _controller.GetFonctions();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Fonction>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Fonction>));
            Assert.AreEqual(3, returnedList.Count(),"Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(f => f.Nom_Fonction == _fonction1.Nom_Fonction), "La fonction 1 est absente");
            CollectionAssert.AreEquivalent(
                _context.Fonctions.Select(f => f.Nom_Fonction).ToList(), 
                returnedList.Select(f => f.Nom_Fonction).ToList(), "Les fonctions retournées sont incorrectes");
        }

        [TestMethod]
        public async Task GetFonctionById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetFonction(_fonction1.Id_Fonction);
            var okResult = action.Result as OkObjectResult;
            var returnedType = okResult.Value as Fonction;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsNotNull(returnedType, "La fonction retournée est nulle");
            Assert.IsInstanceOfType(returnedType, typeof(Fonction), $"Le type de fonction retourné est incorrect {action.Value}");
            Assert.AreEqual(_fonction1.Nom_Fonction, returnedType.Nom_Fonction, "La fonction retournée est incorrecte");
        }

        [TestMethod]
        public async Task GetFonctionById_NonExistingId_Return_NotFound()
        {
            var action = await _controller.GetFonction(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Une fonction a été trouvée");
        }

        [TestMethod]
        public async Task DeleteFonction_Return_NoContent() 
        {
            var action = _controller.DeleteFonction(_fonction2.Id_Fonction);

            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas NoContent");
            Assert.IsNull(_context.Fonctions.Find(_fonction2.Id_Fonction), "La fonction n'a pas été supprimée");
        }

        [TestMethod]
        public async Task DeleteFonction_ReturnNotFound() 
        {
            var action = _controller.DeleteFonction(5);

            Assert.IsNull(_context.Fonctions.Find(5), "Une fonction a été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas NotFound");
        }

        [TestMethod]
        public async Task PostFonction_Return_CreatedAtAction() 
        {
            var action = await _controller.PostFonction(new Fonction { Nom_Fonction = "Fonction4" });
            var foundType = _context.Fonctions.FirstOrDefault(f => f.Nom_Fonction == "Fonction4");

            Assert.IsNotNull(foundType, "La fonction n'a pas été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas CreatedAtAction");
            Assert.IsInstanceOfType(foundType, typeof(Fonction), "Le type de fonction est incorrect");
        }

        [TestMethod]
        public async Task PostFonction_InvalidModelState_ReturnBadRequest() 
        {
            var fonction = new Fonction
            {
                Id_Fonction = -1,
                Nom_Fonction = "Fonction0"
            };
            _controller.ModelState.AddModelError("Id_Fonction", "Id_Fonction invalide");
            var action = await _controller.PostFonction(fonction);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result,typeof(BadRequestObjectResult), "la réponse n'est pas une badRequest");
        }

        [TestMethod]

        public async Task PutFonction_ValidUpdate_ReturnsNoContent() 
        {
            var updatedFonction = new Fonction
            {
                Id_Fonction = _fonction3.Id_Fonction,
                Nom_Fonction = "Fonction3Updated"
            };

            var action = await _controller.PutFonction(_fonction3.Id_Fonction, updatedFonction);
            var fonctionInDb = _context.Fonctions.Find(_fonction3.Id_Fonction);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas NoContentResult");
            Assert.AreEqual(updatedFonction.Nom_Fonction, fonctionInDb.Nom_Fonction, "La fonction n'a pas été mise à jour");
        }

        [TestMethod]
        public async Task PutFonction_InvalidId_ReturnsBadRequest() 
        {
            var updatedFonction = new Fonction
            {
                Id_Fonction = _fonction1.Id_Fonction,
                Nom_Fonction = "Fonction1Updated"
            };
            var action = await _controller.PutFonction(5, updatedFonction);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas BadRequestResult");
        }

        [TestMethod]
        public async Task PutFonction_NonExistingId_ReturnsNotFound() 
        {
            var updatedFonction = new Fonction
            {
                Id_Fonction = 5,
                Nom_Fonction = "Fonction5Updated"
            };
            var action = await _controller.PutFonction(5, updatedFonction);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas NotFoundObjectResult");
        }
    }
}
