using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.DTO;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Domaine_ExcellencesControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Domaine_ExcellencesController _controller;
        private Domaine_ExcellenceManager _manager;

        private Domaine_Excellence _domaine1;
        private Domaine_Excellence _domaine2;
        private Domaine_Excellence _domaine3;

        [TestInitialize]
        public void Intitialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(options);
            _manager = new Domaine_ExcellenceManager(_context);
            _controller = new Domaine_ExcellencesController(_manager, _mapper);

            _domaine1 = new Domaine_Excellence
            {
                intitule_Domaine_Excellence = "Domaine 1",
                Description_Domaine_Excellence = "Description du domaine 1"
            };

            _domaine2 = new Domaine_Excellence
            {
                intitule_Domaine_Excellence = "Domaine 2",
                Description_Domaine_Excellence = "Description du domaine 2"
            };

            _domaine3 = new Domaine_Excellence
            {
                intitule_Domaine_Excellence = "Domaine 3",
                Description_Domaine_Excellence = "Description du domaine 3"
            };

            _context.Domaine_Excellences.AddRange(_domaine1, _domaine2, _domaine3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetDomaines_Return_Ok()
        {
            var action = await _controller.GetDomaine_Excellences();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Domaine_Excellence>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Domaine_Excellence>), "la valeur retournée n'est pas de type IEnumerable<Domaine_Excellences>");
            Assert.AreEqual(3, returnedList.Count(), "Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(d => d.Id_Domaine_Excellence == _domaine1.Id_Domaine_Excellence), "Le domaine attendu n'a pas été trouvé dans la liste retournée");
            CollectionAssert.AreEquivalent(_context.Domaine_Excellences.Select(d => d.Id_Domaine_Excellence).ToList(),
                returnedList.Select(d => d.Id_Domaine_Excellence).ToList(), "Les domaines retournés sont incorrects");
        }

        [TestMethod]
        public async Task GEtDomaineById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetDomaine_Excellence(_domaine2.Id_Domaine_Excellence);
            var okResult = action.Result as OkObjectResult;
            var returnedDomaine = okResult.Value as Domaine_Excellence;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedDomaine, "la valeur retournée est nulle");
            Assert.IsInstanceOfType(returnedDomaine, typeof(Domaine_Excellence), "la valeur retournée n'est pas de type Domaine_Excellence");
            Assert.AreEqual(_domaine2.Id_Domaine_Excellence, returnedDomaine.Id_Domaine_Excellence, "L'Id du domaine retourné est incorrect");
        }

        [TestMethod]
        public async Task GetDomaineById_NonExistingId_Return_NotFound()
        {
            var action = await _controller.GetDomaine_Excellence(999);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(action.Value, "la valeur retournée n'est pas nulle");
        }

        [TestMethod]
        public async Task DeleteDomaine_Return_NoContent()
        {
            var action = await _controller.DeleteDomaine_Excellence(_domaine3.Id_Domaine_Excellence);
            var domaineInDb = await _context.Domaine_Excellences.FindAsync(_domaine3.Id_Domaine_Excellence);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(domaineInDb, "le domaine n'a pas été supprimé de la base de données");
        }

        [TestMethod]
        public async Task DeleteDomaine_Return_NotFound()
        {
            var action = await _controller.DeleteDomaine_Excellence(999);
            var domaineInDb = await _context.Domaine_Excellences.FindAsync(999);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
            Assert.IsNull(domaineInDb, "un domaine a été trouvé");
        }

        [TestMethod]
        public async Task PostDomaine_Return_CreatedAtAction() 
        {
            Domaine_Excellence addDomaine = new Domaine_Excellence
            {
                intitule_Domaine_Excellence = "Domaine à ajouter",
                Description_Domaine_Excellence = "Description du domaine à ajouter"
            };

            var action = await _controller.PostDomaine_Excellence(addDomaine);
            var domaineInDb = await _context.Domaine_Excellences
                .FirstOrDefaultAsync(d => d.intitule_Domaine_Excellence == addDomaine.intitule_Domaine_Excellence);

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(domaineInDb, "le domaine n'a pas été ajouté à la base de données");
            Assert.IsInstanceOfType(domaineInDb, typeof(Domaine_Excellence), "le domaine ajouté n'est pas de type Domaine_Excellence");
        }

        [TestMethod]
        public async Task PostDomaine_Return_BadRequest()
        {
            Domaine_Excellence invalidDomaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 0,
                intitule_Domaine_Excellence = "Domaine à ajouter",
                Description_Domaine_Excellence = "Description du domaine à ajouter"
            };

            _controller.ModelState.AddModelError("Id_Domaine_Excellence", "L'Id ne doit pas être défini lors de la création d'un nouveau domaine.");

            var action = await _controller.PostDomaine_Excellence(invalidDomaine);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(action.Result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestResult");
        }

        [TestMethod]
        public async Task PutDomaine_ValidUpdate_ReturnNoContent() 
        {
            var updatedDomaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 1,
                intitule_Domaine_Excellence = "Domaine1Updated",
                Description_Domaine_Excellence = "Description du domaine Updated"
            };

            var action = await _controller.PutDomaine_Excellence(_domaine1.Id_Domaine_Excellence, updatedDomaine);
            var domaineInDb = _context.Domaine_Excellences.Find(_domaine1.Id_Domaine_Excellence);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(updatedDomaine.intitule_Domaine_Excellence, domaineInDb.intitule_Domaine_Excellence);
        }

        [TestMethod]
        public async Task PutDomaine_NonExisting_ReturnBadRequestResult() 
        {
            var updatedDomaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 1,
                intitule_Domaine_Excellence = "Domaine1Updated",
                Description_Domaine_Excellence = "Description du domaine Updated"
            };

            var action = await _controller.PutDomaine_Excellence(11037, updatedDomaine);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult),"la réponse n'est pas de type BadRequestObject");
        }

        [TestMethod]
        public async Task PutDomaine_NonExistingId_ReturnNotFound() 
        {
            var updatedDomaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 8,
                intitule_Domaine_Excellence = "Domaine1Updated",
                Description_Domaine_Excellence = "Description du domaine Updated"
            };

            var action = await _controller.PutDomaine_Excellence(8, updatedDomaine);

            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult));
        }
    }
}
