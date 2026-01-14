using Microsoft.VisualStudio.TestTools.UnitTesting;
using USMB_TECH.Controllers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECH.Models;
using AutoMapper;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass()]
    [TestCategory("intégration")]
    public class Type_EquipementsControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Type_EquipementsController _controller;
        private Type_EquipementManager _manager;

        private Type_Equipement _type1;
        private Type_Equipement _type2;
        private Type_Equipement _type3;

        [TestInitialize]
        public void Initialize()
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(option);

            _manager = new Type_EquipementManager(_context);
            _controller = new Type_EquipementsController(_manager);

            _type1 = new Type_Equipement { Nom_Type = "Type1" };
            _type2 = new Type_Equipement { Nom_Type = "Type2" };
            _type3 = new Type_Equipement { Nom_Type = "Type3" };

            _context.Type_Equipements.AddRange(_type1, _type2, _type3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllTypes_Equipement_Return_Ok()
        {
            var action = await _controller.GetType_Equipements();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Type_Equipement>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Type_Equipement>), "Les types d'équipements ne sont pas bien récupérés");
            Assert.AreEqual(3, returnedList.Count(), "Ils n'y a pas un nombre de trois types ");
            Assert.IsTrue(returnedList.Any(t => t.Nom_Type == "Type1"), "Le type de prestation Type1 est manquant");
            CollectionAssert.AreEquivalent(
                _context.Type_Equipements.Select(t => t.Nom_Type).ToList(),
                returnedList.Select(t => t.Nom_Type).ToList()
            );
        }

        [TestMethod]
        public async Task GetTypeById_ExistingId_Returns_Ok()
        {
            var action = await _controller.GetType_Equipement(_type2.Id_Type_Equipement);
            var result = action.Result as OkObjectResult;
            var returnedValue = result.Value as Type_Equipement;

            Assert.IsNotNull(result, "La réponse n'est pas un OkObjectResult");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult), "La réponse n'est pas OK");
            Assert.IsNotNull(returnedValue, "Le type d'équipement n'a pas été retourné");
            Assert.AreEqual(_type2.Id_Type_Equipement, returnedValue.Id_Type_Equipement, "L'ID retourné n'est pas le bon");
        }

        [TestMethod]
        public async Task GetTypeById_NonExistingId_Returns_NotFound()
        {
            var action = await _controller.GetType_Equipement(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "La réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Un type d'équipement a été trouvé");
        }

        [TestMethod]
        public async Task DeleteType_Equipement_Return_NoContent()
        {
            var action = _controller.DeleteType_Equipement(_type3.Id_Type_Equipement);
            var typeInDb = _context.Type_Equipements.Find(_type3.Id_Type_Equipement);
            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas de type NotContentResult");
            Assert.IsNull(typeInDb, "Le type d'équipement n'a pas été supprimé");
        }

        [TestMethod]
        public async Task DeleteType_Equipement_ReturnNotFound()
        {
            var action = _controller.DeleteType_Equipement(5);

            Assert.IsNull(_context.Type_Equipements.Find(5), "la réponse n'est pas nulle");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
        }

        [TestMethod]
        public async Task PostType_Equipement_Return_CreatedAtAction()
        {
            var action = await _controller.PostType_Equipement(new Type_Equipement { Nom_Type = "Type4" });

            var foundType = _context.Type_Equipements.FirstOrDefault(t => t.Nom_Type == "Type4");

            Assert.IsNotNull(foundType, "Le type n'a pas été trouvé");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsInstanceOfType(foundType, typeof(Type_Equipement), "Le type trouvé n'est pas un Type_Equipement");
        }

        [TestMethod]
        public async Task PostType_Equipement_InvalidModelState_ReturnsBadRequest()
        {
            var type = new Type_Equipement
            {
                Id_Type_Equipement = -1,
                Nom_Type = "Type0"
            };

            _controller.ModelState.AddModelError("Id_Type_Equipement", "ID invalide");

            var action = await _controller.PostType_Equipement(type);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "La réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "La réponse n'est pas un BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutType_Equipement_ValidUpdate_ReturnsNoContent()
        {
            var updatedType = new Type_Equipement
            {
                Id_Type_Equipement = _type1.Id_Type_Equipement,
                Nom_Type = "Type1Updated"
            };
            var action = await _controller.PutType_Equipement(_type1.Id_Type_Equipement, updatedType);
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "La réponse n'est pas NoContentResult");
            var typeInDb = _context.Type_Equipements.Find(_type1.Id_Type_Equipement);
            Assert.AreEqual(updatedType.Nom_Type, typeInDb.Nom_Type, "Le type d'équipement n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutType_Equipement_InvalidId_ReturnsBadRequest()
        {
            var updatedType = new Type_Equipement
            {
                Id_Type_Equipement = _type1.Id_Type_Equipement,
                Nom_Type = "Type1Updated"
            };
            var action = await _controller.PutType_Equipement(8, updatedType);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "La réponse n'est pas BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutType_Equipement_NonExistingId_ReturnsNotFound() 
        {
            var updatedType = new Type_Equipement
            {
                Id_Type_Equipement = 10,
                Nom_Type = "Type10"
            };
            var action = await _controller.PutType_Equipement(10, updatedType);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "La réponse n'est pas NotFoundObjectResult");
        }
    }
}