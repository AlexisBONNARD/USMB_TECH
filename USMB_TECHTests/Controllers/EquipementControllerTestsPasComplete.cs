
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

namespace UsmbTech.Tests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class EquipementControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private EquipementsController _controller;
        private EquipementManager _manager;

        private Equipement _equipement1;

        private Pole_Expertise _pole;
        private Modele _modele;
        private Marque _marque;
        private Type_Equipement _type;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(options);
            _manager = new EquipementManager(_context);
            _controller = new EquipementsController(_manager, _mapper);

            // Données de référence
            _type = new Type_Equipement
            {
                Id_Type_Equipement = 1,
                Nom_Type = "Type_Equipement"
            };
            _context.Type_Equipements.Add(_type);

            _pole = new Pole_Expertise
            {
                Id_Pole_Expertise = 1,
                Nom_Pole_Expertise = "Pole1",
                Description_Pole_Expertise = "Descr Poles"
            };
            _context.Pole_Expertises.Add(_pole);

            _marque = new Marque
            {
                Id_Marque = 1,
                Nom_Marque = "Marque"
            };
            _context.Marques.Add(_marque);

            _modele = new Modele
            {
                Id_Marque = 1,
                Id_Modele = 1,
                Nom_Modele = "modele"
            };
            _context.Modeles.Add(_modele);

            // Équipement de test
            _equipement1 = new Equipement
            {
                Id_Pole_Expertise = _pole.Id_Pole_Expertise,
                Id_Modele = _modele.Id_Modele,
                Id_Type_Equipement = _type.Id_Type_Equipement,

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
            };

            // ⚠️ Corrige l'AddRange : pas de virgule finale
            _context.Equipements.AddRange(_equipement1);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public async Task GetAllEquipement_Return_Ok()
        {
            var action = await _controller.GetEquipements();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Equipement>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Equipement>), "la valeur retournée n'est pas de type IEnumerable<Equipement>");
            Assert.AreEqual(3, returnedList.Count(), "le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(e => e.Nom_Equipement == _equipement1.Nom_Equipement));
            CollectionAssert.AreEquivalent(_context.Equipements.Select(e => e.Nom_Equipement).ToList(), returnedList.Select(e => e.Nom_Equipement).ToList());
        }

        [TestMethod]
        public async Task GetEquipement_ExistingId_Returns_Ok()
        {
            // Arrange
            var existingId = _equipement1.Id_Equipement; // Id auto-généré par InMemory

            // Act
            var action = await _controller.GetEquipement(existingId);
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
            Assert.AreEqual(existingId, returnedEquipement.Id_Equipement,
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
    }
}
