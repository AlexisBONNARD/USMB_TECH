using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class FonctionnaliteControllerTests
    {
        private UsmbTechDbContext _context;
        private FonctionnaliteManager _manager;
        private FonctionnalitesController _controller;

        private Fonctionnalite _fonction1;
        private Fonctionnalite _fonction2;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(options);
            _manager = new FonctionnaliteManager(_context);
            _controller = new FonctionnalitesController(_manager);

            _fonction1 = new Fonctionnalite
            {
                Nom_Fonctionnalite = "Fct1",
                Description = "Description 1"
            };

            _fonction2 = new Fonctionnalite
            {
                Nom_Fonctionnalite = "Fct2",
                Description = "Description 2"
            };

            _context.Fonctionnalites.AddRange(_fonction1, _fonction2);
            _context.SaveChanges();

            // reload to get generated ids
            _fonction1 = _context.Fonctionnalites.First(f => f.Nom_Fonctionnalite == "Fct1");
            _fonction2 = _context.Fonctionnalites.First(f => f.Nom_Fonctionnalite == "Fct2");
        }

        [TestMethod]
        public async Task GetAllFonctionnalites_Returns_Ok()
        {
            var action = await _controller.Getfonctionnalites();
            var okResult = action.Result as OkObjectResult;
            var returned = okResult?.Value as IEnumerable<Fonctionnalite>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsNotNull(returned);
            Assert.AreEqual(2, returned.Count());
            CollectionAssert.AreEquivalent(_context.Fonctionnalites.Select(f => f.Nom_Fonctionnalite).ToList(), returned.Select(f => f.Nom_Fonctionnalite).ToList());
        }

        [TestMethod]
        public async Task GetFonctionnalite_ExistingId_Returns_Ok()
        {
            var action = await _controller.Getfonctionnalite(_fonction1.Id_Fonctionnalite);
            var okResult = action.Result as OkObjectResult;
            var returned = okResult?.Value as Fonctionnalite;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsNotNull(returned);
            Assert.AreEqual(_fonction1.Nom_Fonctionnalite, returned.Nom_Fonctionnalite);
            Assert.AreEqual(_fonction1.Id_Fonctionnalite, returned.Id_Fonctionnalite);
        }

        [TestMethod]
        public async Task GetFonctionnalite_NonExistingId_Returns_NotFound()
        {
            var action = await _controller.Getfonctionnalite(_fonction1.Id_Fonctionnalite + 999);
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteFonctionnalite_Returns_NoContent()
        {
            var action = await _controller.Deletefonctionnalite(_fonction2.Id_Fonctionnalite);
            var inDb = await _context.Fonctionnalites.FindAsync(_fonction2.Id_Fonctionnalite);

            Assert.IsInstanceOfType(action, typeof(NoContentResult));
            Assert.IsNull(inDb);
        }

        [TestMethod]
        public async Task DeleteFonctionnalite_Returns_NotFound()
        {
            var action = await _controller.Deletefonctionnalite(-999);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PostFonctionnalite_Returns_CreatedAtAction()
        {
            var newF = new Fonctionnalite
            {
                Nom_Fonctionnalite = "FctNew",
                Description = "Desc new"
            };

            var action = await _controller.Postfonctionnalite(newF);
            var created = action.Result as CreatedAtActionResult;
            var inDb = _context.Fonctionnalites.FirstOrDefault(f => f.Nom_Fonctionnalite == newF.Nom_Fonctionnalite);

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));
            Assert.IsNotNull(inDb);
        }

        [TestMethod]
        public async Task PostFonctionnalite_InvalidModelState_Returns_BadRequest()
        {
            var newF = new Fonctionnalite
            {
                Nom_Fonctionnalite = "",
                Description = ""
            };

            _controller.ModelState.AddModelError("Nom_Fonctionnalite", "Le nom est requis.");

            var action = await _controller.Postfonctionnalite(newF);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutFonctionnalite_ValidUpdate_Returns_NoContent()
        {
            var dto = new Fonctionnalite
            {
                Id_Fonctionnalite = _fonction1.Id_Fonctionnalite,
                Nom_Fonctionnalite = "Fct1_Modified",
                Description = "Desc modified"
            };

            var action = await _controller.Putfonctionnalite(_fonction1.Id_Fonctionnalite, dto);
            var inDb = await _context.Fonctionnalites.FindAsync(_fonction1.Id_Fonctionnalite);

            Assert.IsInstanceOfType(action, typeof(NoContentResult));
            Assert.AreEqual(dto.Nom_Fonctionnalite, inDb.Nom_Fonctionnalite);
        }

        [TestMethod]
        public async Task PutFonctionnalite_NonExistingId_Returns_NotFound()
        {
            var notExistingId = _fonction1.Id_Fonctionnalite + 999;
            var dto = new Fonctionnalite
            {
                Id_Fonctionnalite = notExistingId,
                Nom_Fonctionnalite = "DoesNotExist",
                Description = ""
            };

            var action = await _controller.Putfonctionnalite(notExistingId, dto);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutFonctionnalite_IdMismatch_Returns_BadRequest()
        {
            var dto = new Fonctionnalite
            {
                Id_Fonctionnalite = _fonction1.Id_Fonctionnalite + 1,
                Nom_Fonctionnalite = "Mismatch",
                Description = ""
            };

            var action = await _controller.Putfonctionnalite(_fonction1.Id_Fonctionnalite, dto);
            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult));
        }
    }
}
