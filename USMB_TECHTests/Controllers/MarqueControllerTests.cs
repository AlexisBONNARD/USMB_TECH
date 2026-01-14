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

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class MarqueControllerTests
    {

        private UsmbTechDbContext _context;
        private MarqueManager _manager;
        private MarquesController _controller;

        private Marque _marque1;
        private Marque _marque2;
        private Marque _marque3;

        [TestInitialize]
        public void Initialize() 
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
               .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
               .Options;
            _context = new UsmbTechDbContext(option);
            _manager = new MarqueManager(_context);
            _controller = new MarquesController(_manager);
            _marque1 = new Marque { Nom_Marque = "Marque1" };
            _marque2 = new Marque { Nom_Marque = "Marque2" };
            _marque3 = new Marque { Nom_Marque = "Marque3" };
            _context.Marques.AddRange(_marque1, _marque2, _marque3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllMarques_Return_Ok() 
        {
            var action = await _controller.GetMarques();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Marque>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Marque>));
            Assert.AreEqual(3, returnedList.Count(),"Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(m => m.Nom_Marque == "Marque1"),"la Marque1 est absente");
            CollectionAssert.AreEquivalent(
                _context.Marques.Select(m => m.Nom_Marque).ToList(),
                returnedList.Select(m => m.Nom_Marque).ToList(), "Les marques retournées sont incorrectes");
        }

        [TestMethod]
        public async Task GetMarqueById_ExistingId_Return_Ok() 
        {
            var action = await _controller.GetMarque(_marque1.Id_Marque);
            var okResult = action.Result as OkObjectResult;
            var returnedMarque = okResult.Value as Marque;

            Assert.IsInstanceOfType(action.Result, typeof (OkObjectResult), "la réponse n'est pas Ok");
            Assert.IsNotNull(returnedMarque, "la marque est nulle");
            Assert.IsInstanceOfType(returnedMarque, typeof(Marque),"Le type de marque est incorrect");
            Assert.AreEqual(_marque1.Nom_Marque, returnedMarque.Nom_Marque,"La marque retournée est incorrecte");
        }

        [TestMethod]
        public async Task GetMarqueById_NonExistingId_Return_NotFound() 
        {
            var action = await _controller.GetMarque(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas NotFound");
            Assert.IsNull(action.Value, "Une marque a été trouvée");
        }

        [TestMethod]
        public async Task DeleteMarque_Return_NoContent()
        {
            var action = _controller.DeleteMarque(_marque2.Id_Marque);
            var marqueInDb = _context.Marques.Find(_marque2.Id_Marque);
            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas NoContent");
            Assert.IsNull(marqueInDb, "La marque n'a pas été supprimée");
        }

        [TestMethod]
        public async Task DeleteMarque_ReturnNotFound()
        {
            var action = _controller.DeleteMarque(5);

            Assert.IsNull(_context.Marques.Find(5), "Une marque a été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundObjectResult), "la réponse n'est pas NotFound");
        }

        [TestMethod]
        public async Task PostMarque_Return_CreatedAtAction()
        {
            var action = await _controller.PostMarque(new Marque { Nom_Marque = "Marque4" });
            var foundMarque = _context.Marques.FirstOrDefault(f => f.Nom_Marque == "Marque4");

            Assert.IsNotNull(foundMarque, "La marque n'a pas été trouvée");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas CreatedAtAction");
            Assert.IsInstanceOfType(foundMarque, typeof(Marque), "Le type de marque est incorrect");
        }

        [TestMethod]
        public async Task PostMarque_InvalidModelState_ReturnBadRequest()
        {
            var marque = new Marque
            {
                Id_Marque = -1,
                Nom_Marque = "Marque0"
            };
            _controller.ModelState.AddModelError("Id_Marque", "Id_Marque invalide");
            var action = await _controller.PostMarque(marque);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas une badRequest");
        }

        [TestMethod]
        public async Task PutMarque_ValidUpdate_ReturnsNoContent()
        {
            var updatedMarque = new Marque
            {
                Id_Marque = _marque3.Id_Marque,
                Nom_Marque = "Marque3Updated"
            };

            var action = await _controller.PutMarque(_marque3.Id_Marque, updatedMarque);
            var marqueInDb = _context.Marques.Find(_marque3.Id_Marque);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas NoContentResult");
            Assert.AreEqual(updatedMarque.Nom_Marque, marqueInDb.Nom_Marque, "La marque n'a pas été mise à jour");
        }

        [TestMethod]
        public async Task PutMarque_InvalidId_ReturnsBadRequest()
        {
            var updatedMarque = new Marque
            {
                Id_Marque = _marque1.Id_Marque,
                Nom_Marque = "Marque1Updated"
            };
            var action = await _controller.PutMarque(5, updatedMarque);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas BadRequestResult");
        }

        [TestMethod]
        public async Task PutMarque_NonExistingId_ReturnsNotFound()
        {
            var updatedMarque = new Marque
            {
                Id_Marque = 5,
                Nom_Marque = "Marque5Updated"
            };
            var action = await _controller.PutMarque(5, updatedMarque);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas NotFoundObjectResult");
        }
    }
}
