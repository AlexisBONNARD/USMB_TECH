using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Controllers;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Mot_clefsControllerTests
    {
        private UsmbTechDbContext _context;
        private MotClefManager _manager;
        private Mot_ClefsController _controller;

        private Mot_Clef _mot1;
        private Mot_Clef _mot2;
        private Mot_Clef _mot3;

        [TestInitialize]
        public void Initialize() 
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(option);
            _manager = new MotClefManager(_context);
            _controller = new Mot_ClefsController(_manager);

            _mot1 = new Mot_Clef { Nom_Mot_Clef = "mot1" };
            _mot2 = new Mot_Clef { Nom_Mot_Clef = "mot2" };
            _mot3 = new Mot_Clef { Nom_Mot_Clef = "mot3" };
            _context.Mot_Clefs.AddRange(_mot1, _mot2, _mot3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllMot_Clefs_Return_Ok()
        {
            var action = await _controller.Getmot_clefs();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Mot_Clef>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Mot_Clef>));
            Assert.AreEqual(3, returnedList.Count(), "Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(m => m.Nom_Mot_Clef == "mot1"), "le mot clé1 est absent");
            CollectionAssert.AreEquivalent(
                _context.Mot_Clefs.Select(m => m.Nom_Mot_Clef).ToList(),
                returnedList.Select(m => m.Nom_Mot_Clef).ToList(), "Les mots clés retournés sont incorrectes");
        }

        [TestMethod]
        public async Task GetMot_ClefById_ExistingId_Return_Ok()
        {
            var action = await _controller.Getmot_clef(_mot1.Id_Mot_Clef);
            var okResult = action.Result as OkObjectResult;
            var returnedMot = okResult.Value as Mot_Clef;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas Ok");
            Assert.IsNotNull(returnedMot, "le mot clé est null");
            Assert.IsInstanceOfType(returnedMot, typeof(Mot_Clef), "Le type de mot clé est incorrect");
            Assert.AreEqual(_mot1.Nom_Mot_Clef, returnedMot.Nom_Mot_Clef, "Le mot clé retourné est incorrecte");
        }

        [TestMethod]
        public async Task GetMarqueById_NonExistingId_Return_NotFound()
        {
            var action = await _controller.Getmot_clef(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Un mot clé a été trouvé");
        }

        [TestMethod]
        public async Task DeleteMot_Clef_Return_NoContent()
        {
            var action = _controller.Deletemot_clef(_mot1.Id_Mot_Clef);

            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas NoContent");
            Assert.IsNull(_context.Fonctions.Find(_mot1.Id_Mot_Clef), "Le mot clé n'a pas été supprimé");
        }

        [TestMethod]
        public async Task DeleteMot_Clef_ReturnNotFound()
        {
            var action = _controller.Deletemot_clef(5);

            Assert.IsNull(_context.Mot_Clefs.Find(5), "Un mot_clé a été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas NotFound");
        }

        [TestMethod]
        public async Task PostMot_Clef_Return_CreatedAtAction()
        {
            var action = await _controller.Postmot_clef(new Mot_Clef { Nom_Mot_Clef = "Mot4" });
            var foundMot = _context.Mot_Clefs.FirstOrDefault(f => f.Nom_Mot_Clef == "Mot4");

            Assert.IsNotNull(foundMot, "Le mot clé n'a pas été trouvé");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas CreatedAtAction");
            Assert.IsInstanceOfType(foundMot, typeof(Mot_Clef), "Le type de mot clé est incorrect");
        }

        [TestMethod]
        public async Task PostMot_Clef_InvalidModelState_ReturnBadRequest()
        {
            var mot = new Mot_Clef
            {
                Id_Mot_Clef = -1,
                Nom_Mot_Clef = "mot0"
            };
            _controller.ModelState.AddModelError("Id_Mot_Clef", "Id_Mot_Clef invalide");
            var action = await _controller.Postmot_clef(mot);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas une badRequest");
        }

        [TestMethod]
        public async Task PutMot_Clef_ValidUpdate_ReturnsNoContent()
        {
            var updatedMot = new Mot_Clef
            {
                Id_Mot_Clef = _mot3.Id_Mot_Clef,
                Nom_Mot_Clef = "Mot3Updated"
            };

            var action = await _controller.Putmot_clef(_mot3.Id_Mot_Clef, updatedMot);
            var motInDb = _context.Mot_Clefs.Find(_mot3.Id_Mot_Clef);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas NoContentResult");
            Assert.AreEqual(updatedMot.Nom_Mot_Clef, motInDb.Nom_Mot_Clef, "Le nom n'a pas été mise à jour");
        }

        [TestMethod]
        public async Task PutMot_Clef_InvalidId_ReturnsBadRequest()
        {
            var updatedMot = new Mot_Clef
            {
                Id_Mot_Clef = _mot1.Id_Mot_Clef,
                Nom_Mot_Clef = "Mot1Updated"
            };
            var action = await _controller.Putmot_clef(5, updatedMot);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas BadRequestResult");
        }

        [TestMethod]
        public async Task PutMot_Clef_NonExistingId_ReturnsNotFound()
        {
            var updatedMot = new Mot_Clef
            {
                Id_Mot_Clef = 5,
                Nom_Mot_Clef = "Mot5Updated"
            };
            var action = await _controller.Putmot_clef(5, updatedMot);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas NotFoundObjectResult");
        }
    }
}
