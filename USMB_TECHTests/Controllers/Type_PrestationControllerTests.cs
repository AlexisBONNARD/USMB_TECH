using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass()]
    [TestCategory("intégration")]
    public class Type_PrestationControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Type_PrestationsController _controller;
        private Type_PrestationManager _manager;

        private Type_Prestation _type1;
        private Type_Prestation _type2;
        private Type_Prestation _type3;

        [TestInitialize]
        public void Initialize()
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
               .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
               .Options;
            _context = new UsmbTechDbContext(option);

            _manager = new Type_PrestationManager(_context);
            _controller = new Type_PrestationsController(_manager);

            _type1 = new Type_Prestation { Nom_Type_Prestation = "Type1" };
            _type2 = new Type_Prestation { Nom_Type_Prestation = "Type2" };
            _type3 = new Type_Prestation { Nom_Type_Prestation = "Type3" };

            _context.Type_Prestations.AddRange(_type1, _type2, _type3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllTypes_Prestation_Return_Ok()
        {
            var action = await _controller.GetType_Prestations();

            var okResult = action.Result as OkObjectResult;

            var returnedList = okResult.Value as IEnumerable<Type_Prestation>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Type_Prestation>), "Les types de prestations ne sont pas bien récupérés");
            Assert.AreEqual(3, returnedList.Count(), "Le nombre de types de prestations retournés est incorrect");
            Assert.IsTrue(returnedList.Any(t => t.Nom_Type_Prestation == "Type1"), "Le type de prestation Type1 est manquant");
            CollectionAssert.AreEquivalent(
                _context.Type_Prestations.Select(t => t.Nom_Type_Prestation).ToList(),
                returnedList.Select(t => t.Nom_Type_Prestation).ToList(), "Les types de prestations retournés ne correspondent pas à ceux de la base de données"
                );
        }

        [TestMethod]
        public async Task GetTypeById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetType_Prestation(_type2.Id_Type_Prestation);
            var okResult = action.Result as OkObjectResult;
            var returnedType = okResult.Value as Type_Prestation;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsNotNull(returnedType, "Le type de prestation retourné est null");
            Assert.AreEqual(_type2.Nom_Type_Prestation, returnedType.Nom_Type_Prestation, "Le type de prestation retourné est incorrect");
        }

        [TestMethod]
        public async Task GetTypeById_NonExistingId_Returns_NotFound()
        {
            var action = await _controller.GetType_Prestation(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "La réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Un type de prestation a été trouvé");
        }

        [TestMethod]
        public async Task DeleteType_Prestation_Return_NoContent()
        {
            var action = _controller.DeleteType_Prestation(_type3.Id_Type_Prestation);

            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas de type NotContentResult");
            Assert.IsNull(_context.Type_Prestations.Find(_type3.Id_Type_Prestation), "Le type de la prestation n'a pas été supprimé");
        }

        [TestMethod]
        public async Task DeleteType_Prestation_ReturnNotFound()
        {
            var action = _controller.DeleteType_Prestation(5);

            Assert.IsNull(_context.Type_Prestations.Find(5), "la réponse n'est pas nulle");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
        }

        [TestMethod]
        public async Task PostType_Prestation_Return_CreatedAtAction()
        {
            var action = await _controller.PostType_Prestation(new Type_Prestation { Nom_Type_Prestation = "Type4" });

            var foundType = _context.Type_Prestations.FirstOrDefault(t => t.Nom_Type_Prestation == "Type4");

            Assert.IsNotNull(foundType, "Le type n'a pas été trouvé");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsInstanceOfType(foundType, typeof(Type_Prestation), "Le type trouvé n'est pas un Type_Prestation");
        }

        [TestMethod]
        public async Task PostType_Prestation_InvalidModelState_ReturnsBadRequest()
        {
            var type = new Type_Prestation
            {
                Id_Type_Prestation = -1,
                Nom_Type_Prestation = "Type0"
            };
            _controller.ModelState.AddModelError("Id_Type_Prestation", "ID invalide");
            var action = await _controller.PostType_Prestation(type);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "La réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "La réponse n'est pas un BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutType_Prestation_ValidUpdate_ReturnsNoContent()
        {
            var updatedType = new Type_Prestation
            {
                Id_Type_Prestation = _type1.Id_Type_Prestation,
                Nom_Type_Prestation = "Type1Updated"
            };
            var action = await _controller.PutType_Prestation(_type1.Id_Type_Prestation, updatedType);
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "La réponse n'est pas NoContentResult");
            var typeInDb = _context.Type_Prestations.Find(_type1.Id_Type_Prestation);
            Assert.AreEqual(updatedType.Nom_Type_Prestation, typeInDb.Nom_Type_Prestation, "Le type de prestation n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutType_Prestation_InvalidId_ReturnsBadRequest()
        {
            var updatedType = new Type_Prestation
            {
                Id_Type_Prestation = _type1.Id_Type_Prestation,
                Nom_Type_Prestation = "Type1Updated"
            };
            var action = await _controller.PutType_Prestation(8, updatedType);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "La réponse n'est pas BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutType_Prestation_NonExistingId_ReturnsNotFound()
        {
            var updatedType = new Type_Prestation
            {
                Id_Type_Prestation = 10,
                Nom_Type_Prestation = "Type10"
            };
            var action = await _controller.PutType_Prestation(10, updatedType);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "La réponse n'est pas NotFoundObjectResult");
        }
    }
}
