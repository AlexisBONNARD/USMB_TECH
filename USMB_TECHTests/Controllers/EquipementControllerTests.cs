using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;
using System.Linq;
using System.Collections.Generic;
using USMB_TECH.DTO;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class EquipementControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private EquipementsController _controller;
        private EquipementManager _manager;

        private Equipement _equipement1;

        private Pole_Expertise pole;
        private Modele modele;
        private Marque marque;
        private Type_Equipement type;
        private Domaine_Excellence domaine;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(options);
            _manager = new EquipementManager(_context);
            _controller = new EquipementsController(_manager, _mapper);

            domaine = new Domaine_Excellence
            {
                Id_Domaine_Excellence = 1,
                intitule_Domaine_Excellence = "Domaine1",
                Description_Domaine_Excellence = "Description domaine"
            };
            _context.Domaine_Excellences.Add(domaine);

            type = new Type_Equipement
            {
                Id_Type_Equipement = 1,
                Nom_Type = "Type_Equipement"
            };
            _context.Type_Equipements.Add(type);

            pole = new Pole_Expertise
            {
                Id_Pole_Expertise = 1,
                Id_Domaine_Excellence = domaine.Id_Domaine_Excellence,
                Nom_Pole_Expertise = "Pole1",
                Description_Pole_Expertise = "Descr Poles",
                Actif = true,
                Domaine_ExcellenceNavigation = domaine
            };
            _context.Pole_Expertises.Add(pole);


            marque = new Marque
            {
                Id_Marque = 1,
                Nom_Marque = "Marque"
            };

            _context.Marques.Add(marque);

            modele = new Modele
            {
                Id_Modele = 1,
                Id_Marque = marque.Id_Marque,
                Nom_Modele = "modele",
                MarqueNavigation = marque
            };

            _context.Modeles.Add(modele);


            _equipement1 = new Equipement
            {
                Id_Pole_Expertise = pole.Id_Pole_Expertise,
                Id_Modele = modele.Id_Modele,
                Id_Type_Equipement = type.Id_Type_Equipement,

                Nom_Equipement = "Equipement1",
                Num_Immobilisation = "1",
                Description_Technique = "Description Equipement1",
                Url_Modele_3D = "https://url.com",

                Date_Acquisition = DateTime.Today,
                Prix_Achat = 1.0,
                Prix_Revient = 1.0,

                Disponibilite = true,
                Autonomie = true,
                Utilisable_Chez_Le_Client = true,
                Actif = true,

                Pole_ExpertiseNavigation = pole,
                ModeleNavigation = modele,
                Type_EquipementNavigation = type
            };
            _context.Equipements.AddRange(_equipement1);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllEquipement_Return_Ok()
        {
            var action = await _controller.GetEquipements();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Equipement>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Equipement>), "la valeur retournée n'est pas de type IEnumerable<Equipement>");
            Assert.AreEqual(1, returnedList.Count(), "le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(e => e.Nom_Equipement == _equipement1.Nom_Equipement));
            CollectionAssert.AreEquivalent(_context.Equipements.Select(e => e.Nom_Equipement).ToList(), returnedList.Select(e => e.Nom_Equipement).ToList());
        }

        [TestMethod]
        public async Task GetEquipement_ExistingId_Returns_Ok()
        {
            var action = await _controller.GetEquipement(_equipement1.Id_Equipement);
            var okResult = action.Result as OkObjectResult;
            var returnedEquipement = okResult?.Value as Equipement;

            // Assert
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult),
                "La réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedEquipement, "L'équipement retourné est null");
            Assert.IsInstanceOfType(returnedEquipement, typeof(Equipement),
                "La valeur retournée n'est pas de type Equipement");
            Assert.AreEqual(_equipement1.Nom_Equipement, returnedEquipement.Nom_Equipement,
                "Le nom de l'équipement retourné est incorrect");
            Assert.AreEqual(_equipement1.Id_Equipement, returnedEquipement.Id_Equipement,
                "L'Id de l'équipement retourné est incorrect");
        }

        [TestMethod]
        public async Task GetEquipement_NotExistingId_Returns_NotFound()
        {
            // Arrange
            var notExistingId = _equipement1.Id_Equipement + 999;

            // Act
            var action = await _controller.GetEquipement(notExistingId);

            // Assert
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult),
                "La réponse aurait dû être NotFoundResult pour un id inexistant");
        }

        [TestMethod]
        public async Task DeleteEquipement_Return_NoContent()
        {
            var action = await _controller.DeleteEquipement(_equipement1.Id_Equipement);
            var equipementInDb = await _context.Equipements.FindAsync(_equipement1.Id_Equipement);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(equipementInDb, "l'équipement n'a pas été supprimé de la base de données");
        }

        [TestMethod]
        public async Task DeleteEquipement_Return_NotFound()
        {
            var action = await _controller.DeleteEquipement(-999);
            var equipementInDb = await _context.Equipements.FindAsync(-999);

            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
            Assert.IsNull(equipementInDb, "un équipement inexistant a été trouvé dans la base de données");
        }

        [TestMethod]
        public async Task PostEquipement_Return_CreatedAtAction()
        {
            var dto = new AddEquipementDTO
            {
                Nom_Equipement = "Equipement2",
                Num_Immobilisation = "2",
                Date_Acquisition = DateTime.Today,
                Description_Technique = "Description Equipement2",
                Prix_Achat = 10.0,
                Prix_Revient = 12.0,
                Nom_Pole_Expertise = pole.Nom_Pole_Expertise,
                Nom_Modele = modele.Nom_Modele,
                Nom_Marque = marque.Nom_Marque,
                Type_Equipement = type.Nom_Type,
                Nom_Exemple = "Exemple Equipement",
                Description_Exemple = "Description Exemple",

            };

            var action = await _controller.PostEquipement(dto);
            var createdResult = action.Result as CreatedAtActionResult;
            var equipementInDb = _context.Equipements.FirstOrDefault(e => e.Nom_Equipement == dto.Nom_Equipement);

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(equipementInDb, "l'équipement n'a pas été ajouté à la base de données");
            Assert.IsInstanceOfType(equipementInDb, typeof(Equipement), "l'objet ajouté n'est pas de type Equipement");
        }

        [TestMethod]
        public async Task PostEquipement_InvalidModelState_ReturnBadRequest()
        {
            var dto = new AddEquipementDTO
            {
                Nom_Equipement = "Equipement3",
                Num_Immobilisation = "3",
                Date_Acquisition = DateTime.Today,
                Description_Technique = "Description Equipement3",
                Prix_Achat = 5.0,
                Prix_Revient = 7.0,
                Nom_Pole_Expertise = pole.Nom_Pole_Expertise,
                Nom_Modele = modele.Nom_Modele,
                Nom_Marque = marque.Nom_Marque,
                Type_Equipement = type.Nom_Type
            };

            _controller.ModelState.AddModelError("Nom_Equipement", "Le nom est requis.");

            var action = await _controller.PostEquipement(dto);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutEquipement_ValidUpdate_ReturnNoContent()
        {
            var dto = new UpdateEquipementDto
            {
                Id_Equipement = _equipement1.Id_Equipement,
                Nom_Equipement = "Equipement1_Modified",
                Id_Type_Equipement = type.Id_Type_Equipement,
                Utilisable_Chez_Le_Client = false
            };

            var action = await _controller.PutEquipement(_equipement1.Id_Equipement, dto);
            var equipementInDb = await _context.Equipements.FindAsync(_equipement1.Id_Equipement);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(dto.Nom_Equipement, equipementInDb.Nom_Equipement, "le nom de l'équipement n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutEquipement_NonExistingId_ReturnNotFound()
        {
            var notExistingId = _equipement1.Id_Equipement + 999;
            var dto = new UpdateEquipementDto
            {
                Id_Equipement = notExistingId,
                Nom_Equipement = "DoesNotExist",
                Id_Type_Equipement = type.Id_Type_Equipement,
                Utilisable_Chez_Le_Client = true
            };

            var action = await _controller.PutEquipement(notExistingId, dto);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
        }

        [TestMethod]
        public async Task PutEquipement_IdMismatch_ReturnBadRequest()
        {
            var dto = new UpdateEquipementDto
            {
                Id_Equipement = _equipement1.Id_Equipement + 1,
                Nom_Equipement = "Mismatch",
                Id_Type_Equipement = type.Id_Type_Equipement,
                Utilisable_Chez_Le_Client = true
            };

            var action = await _controller.PutEquipement(_equipement1.Id_Equipement, dto);
            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestResult");
        }
    }
}
