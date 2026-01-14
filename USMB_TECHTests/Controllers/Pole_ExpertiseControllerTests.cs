using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;
using USMB_TECH.DTO;
using System.Collections.Generic;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Pole_ExpertiseControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Pole_ExpertisesController _controller;
        private Pole_ExpertiseManager _manager;

        private Pole_Expertise _pole1;
        private Domaine_Excellence _domaine;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(options);
            _manager = new Pole_ExpertiseManager(_context);
            _controller = new Pole_ExpertisesController(_manager, _context);

            _domaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 1,
                intitule_Domaine_Excellence = "Domaine1",
                Description_Domaine_Excellence = "Desc Domaine"
            };
            _context.Domaine_Excellences.Add(_domaine);

            _pole1 = new Pole_Expertise
            {
                Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence,
                Nom_Pole_Expertise = "Pole1",
                Description_Pole_Expertise = "Description Pole 1",
                Actif = true,
                Domaine_ExcellenceNavigation = _domaine
            };

            _context.Pole_Expertises.Add(_pole1);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllPoleExpertise_Return_Ok()
        {
            var action = await _controller.GetPole_Expertises();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult?.Value as IEnumerable<Pole_Expertise>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedList, "la valeur retournée est nulle");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Pole_Expertise>), "la valeur retournée n'est pas de type IEnumerable<Pole_Expertise>");
            Assert.AreEqual(1, returnedList.Count(), "le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(p => p.Nom_Pole_Expertise == _pole1.Nom_Pole_Expertise));
        }

        [TestMethod]
        public async Task GetPoleExpertiseById_ExistingId_Returns_Ok()
        {
            var action = await _controller.GetPole_Expertise(_pole1.Id_Pole_Expertise);
            var okResult = action.Result as OkObjectResult;
            var returned = okResult?.Value as Pole_Expertise;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returned, "la pole expertise retournée est nulle");
            Assert.AreEqual(_pole1.Nom_Pole_Expertise, returned.Nom_Pole_Expertise);
        }

        [TestMethod]
        public async Task GetPoleExpertiseById_NonExistingId_Returns_NotFound()
        {
            var action = await _controller.GetPole_Expertise(_pole1.Id_Pole_Expertise + 999);
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundResult");
        }

        [TestMethod]
        public async Task DeletePoleExpertise_Return_NoContent()
        {
            var action = await _controller.DeletePole_Expertise(_pole1.Id_Pole_Expertise);
            var inDb = await _context.Pole_Expertises.FindAsync(_pole1.Id_Pole_Expertise);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(inDb, "la pole expertise n'a pas été supprimée");
        }

        [TestMethod]
        public async Task DeletePoleExpertise_Return_NotFound()
        {
            var action = await _controller.DeletePole_Expertise(-999);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
        }

        //[TestMethod]
        //public async Task PostPoleExpertise_Return_CreatedAtAction()
        //{
        //    var dto = new AddPole_ExpertiseDto
        //    {
        //        Nom_Pole_Expertise = "PoleNew",
        //        Description_Pole_Expertise = "Desc",
        //        Actif = true,
        //        Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence,
        //        MotsCles = new List<string> { "tag1", "tag2" }
        //    };

        //    var action = await _controller.PostPole_Expertise(dto);
        //    var created = action.Result as CreatedAtActionResult;
        //    var inDb = _context.Pole_Expertises.FirstOrDefault(p => p.Nom_Pole_Expertise == dto.Nom_Pole_Expertise);

        //    Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
        //    Assert.IsNotNull(inDb, "la pole expertise n'a pas été ajoutée");
        //}

        //[TestMethod]
        //public async Task PostPoleExpertise_InvalidModelState_ReturnBadRequest()
        //{
        //    var dto = new AddPole_ExpertiseDto
        //    {
        //        Nom_Pole_Expertise = "",
        //        Description_Pole_Expertise = "",
        //        Actif = true,
        //        Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence
        //    };

        //    _controller.ModelState.AddModelError("Nom_Pole_Expertise", "Le nom est requis.");

        //    var action = await _controller.PostPole_Expertise(dto);
        //    var result = action.Result as BadRequestObjectResult;

        //    Assert.IsNotNull(result, "la réponse est nulle");
        //    Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        //}

        [TestMethod]
        public async Task PutPoleExpertise_ValidUpdate_ReturnNoContent()
        {
            var dto = new UpdatePole_ExpertiseDto
            {
                Id_Pole_Expertise = _pole1.Id_Pole_Expertise,
                Nom_Pole_Expertise = "Pole1_Modified",
                Description_Pole_Expertise = "Desc Modified",
                Actif = false,
                Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence
            };

            var action = await _controller.PutPole_Expertise(_pole1.Id_Pole_Expertise, dto);
            var inDb = await _context.Pole_Expertises.FindAsync(_pole1.Id_Pole_Expertise);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(dto.Nom_Pole_Expertise, inDb.Nom_Pole_Expertise, "le nom n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutPoleExpertise_NonExistingId_ReturnNotFound()
        {
            var notExistingId = _pole1.Id_Pole_Expertise + 999;
            var dto = new UpdatePole_ExpertiseDto
            {
                Id_Pole_Expertise = notExistingId,
                Nom_Pole_Expertise = "DoesNotExist",
                Description_Pole_Expertise = "",
                Actif = true,
                Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence
            };

            var action = await _controller.PutPole_Expertise(notExistingId, dto);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
        }

        [TestMethod]
        public async Task PutPoleExpertise_IdMismatch_ReturnBadRequest()
        {
            var dto = new UpdatePole_ExpertiseDto
            {
                Id_Pole_Expertise = _pole1.Id_Pole_Expertise + 1,
                Nom_Pole_Expertise = "Mismatch",
                Description_Pole_Expertise = "",
                Actif = true,
                Id_Domaine_Excellence = _domaine.Id_Domaine_Excellence
            };

            var action = await _controller.PutPole_Expertise(_pole1.Id_Pole_Expertise, dto);
            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestResult");
        }
    }
}
