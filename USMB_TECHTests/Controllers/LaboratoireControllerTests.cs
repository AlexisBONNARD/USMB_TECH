using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Models;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Controllers;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.DTO;
using USMB_TECHTests.AutoMapper;
using Microsoft.Build.Framework;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class LaboratoireControllerTests : AutoMapperConfigTests
    {
        public UsmbTechDbContext _context;
        public LaboratoiresController _controller;
        public LaboratoireManager _manager;

        public Laboratoire _labo1;
        public Laboratoire _labo2;
        public Laboratoire _labo3;

        public Adresse adresse;
        public Pole_Expertise pole;
        public Mot_Clef mot;
        public Thematique theme;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(options);
            _manager = new LaboratoireManager(_context);
            _controller = new LaboratoiresController(_manager, _mapper);

            adresse = new Adresse { Id_Adresse = 1, Rue_Adresse = "1 rue de l'université", Ville_Adresse = "Chambéry", Code_Postal_Adresse = "73000", Pays_Adresse = "France" };
            pole = new Pole_Expertise { Id_Pole_Expertise = 1, Nom_Pole_Expertise = "Informatique", Description_Pole_Expertise = "descr" };
            mot = new Mot_Clef { Id_Mot_Clef = 1, Nom_Mot_Clef = "Informatique" };
            theme = new Thematique { Id_Thematique = 1, Nom_Thematique = "IA" };
            _context.Adresses.Add(adresse);
            _context.Pole_Expertises.Add(pole);

            _labo1 = new Laboratoire
            {
                Nom_Court = "Labo1",
                Nom_Long = "Laboratoire 1",
                Description = "Description du laboratoire 1",
                Id_Adresse_Campus = adresse.Id_Adresse,
                Id_Adresse_Labo = adresse.Id_Adresse,
            };

            _labo2 = new Laboratoire
            {
                Nom_Court = "Labo2",
                Nom_Long = "Laboratoire 2",
                Description = "Description du laboratoire 2",
                Id_Adresse_Campus = adresse.Id_Adresse,
                Id_Adresse_Labo = adresse.Id_Adresse,
            };

            _labo3 = new Laboratoire
            {
                Nom_Court = "Labo3",
                Nom_Long = "Laboratoire 3",
                Description = "Description du laboratoire 3",
                Id_Adresse_Campus = adresse.Id_Adresse,
                Id_Adresse_Labo = adresse.Id_Adresse,
            };


            _context.Laboratoires.AddRange(_labo1, _labo2, _labo3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllLaboratoire_Return_Ok()
        {
            var action = await _controller.GetLaboratoires();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Laboratoire>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Laboratoire>), "la valeur retournée n'est pas de type IEnumerable<Laboratoire>");
            Assert.AreEqual(3, returnedList.Count(), "le nombre de laboratoires retournés est incorrect");
            Assert.IsTrue(returnedList.Any(l => l.Nom_Court == _labo1.Nom_Court), "le laboratoire 1 n'a pas été trouvé");
            CollectionAssert.AreEquivalent(_context.Laboratoires.Select(l => l.Nom_Court).ToList(), returnedList.Select(l => l.Nom_Court).ToList(), "les laboratoires retournés ne correspondent pas à ceux de la base de données");
        }

        [TestMethod]
        public async Task GetLaboratoireById_ExistingId_Rreturn_Ok()
        {
            var action = await _controller.GetLaboratoire(_labo2.Nom_Court);
            var okResult = action.Result as OkObjectResult;
            var returnedLabo = okResult.Value as Laboratoire;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedLabo, typeof(Laboratoire), "la valeur retournée n'est pas de type Laboratoire");
            Assert.AreEqual(_labo2.Nom_Court, returnedLabo.Nom_Court, "le laboratoire retourné n'est pas le bon");
        }

        [TestMethod]
        public async Task GetLaboratoireById_NonExistingId_return_NotFound()
        {
            var action = await _controller.GetLaboratoire("LaboInexistant");

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundObjectResult");
            Assert.IsNull(action.Value, "Un la boratoire a été trouvé");
        }

        [TestMethod]
        public async Task DeleteLaboratoire_Return_NoContent()
        {
            var action = await _controller.DeleteLaboratoire(_labo3.Nom_Court);
            var laboInDb = await _context.Laboratoires.FindAsync(_labo3.Nom_Court);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(laboInDb, "le laboratoire n'a pas été supprimé de la base de données");
        }

        [TestMethod]
        public async Task DeleteLaboratoire_Return_NotFound()
        {
            var action = await _controller.DeleteLaboratoire("LaboInexistant");
            var laboInDb = await _context.Laboratoires.FindAsync("LaboInexistant");
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(laboInDb, "un laboratoire inexistant a été trouvé dans la base de données");
        }

        [TestMethod]
        public async Task PostLaboratoire_Return_CreatedAtAction()
        {
            var action = await _controller.PostLaboratoire(new AddLaboratoireDTO
            {
                Nom_Court = "Labo4",
                Nom_Long = "Laboratoire 4",
                Description = "Description du laboratoire 4",
                Rue_Adresse_Campus = "2 rue de l'université",
                Ville_Adresse_Campus = "Chambéry",
                Code_Postal_Adresse_Campus = "73000",
                Pays_Adresse_Campus = "France",
                Rue_Adresse_Labo = "3 rue de l'université",
                Ville_Adresse_Labo = "Chambéry",
                Code_Postal_Adresse_Labo = "73000",
                Pays_Adresse_Labo = "France",
                mot_Clefs = new List<string> { "Informatique" },
                Thematiques = new List<string> { "IA" },
                Pole_Expertises = new List<string> { pole.Nom_Pole_Expertise }
            });

            var laboratoireInDb = await _context.Laboratoires.FindAsync("Labo4");

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(laboratoireInDb, "le laboratoire n'a pas été ajouté à la base de données");
            Assert.IsInstanceOfType(laboratoireInDb, typeof(Laboratoire), "l'objet ajouté n'est pas de type Laboratoire");
        }

        [TestMethod]
        public async Task PostLaboratoire_InvalidModelState_ReturnBadRequest()
        {

            AddLaboratoireDTO lab = new AddLaboratoireDTO
            {
                Nom_Court = "Labo4",
                Nom_Long = "Laboratoire 4",
                Description = "",
                Rue_Adresse_Campus = "2 rue de l'université",
                Ville_Adresse_Campus = "Chambéry",
                Code_Postal_Adresse_Campus = "73000",
                Pays_Adresse_Campus = "France",
                Rue_Adresse_Labo = "3 rue de l'université",
                Ville_Adresse_Labo = "Chambéry",
                Code_Postal_Adresse_Labo = "73000",
                Pays_Adresse_Labo = "France",
                mot_Clefs = new List<string> { "Informatique", "Réseaux" },
                Thematiques = new List<string> { "IA", "Sécurité" },
                Pole_Expertises = new List<string> { pole.Nom_Pole_Expertise }
            };

            _controller.ModelState.AddModelError("Description", "La description est obligatoire.");

            var action = await _controller.PostLaboratoire(lab);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutLaboratoire_ValidUpdate_ReturnNoContent()
        {
            LaboratoireUpdateDTO dto = new LaboratoireUpdateDTO
            {
                Nom_Court = _labo1.Nom_Court,
                Nom_Long = "Laboratoire Informatique et Réseaux",
                Description = "Mise à jour du laboratoire spécialisé en IA et cybersécurité.",

                Id_Adresse_Campus = 1,
                Id_Adresse_Labo = 1,

                Pole_Expertises = new List<int> { 1 },
                mot_Clefs = new List<int> { 1 },
                Thematiques = new List<int> { 1 }
            };

            var action = await _controller.PutLaboratoire(_labo1.Nom_Court, dto);
            var laboInDb = await _context.Laboratoires.FindAsync(_labo1.Nom_Court);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(dto.Nom_Long, laboInDb.Nom_Long, "le nom long du laboratoire n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutLaboratoire_NonExistingId_ReturnNotFound()
        {
            LaboratoireUpdateDTO dto = new LaboratoireUpdateDTO
            {
                Nom_Court = "LaboInexistant",
                Nom_Long = "Laboratoire Inexistant",
                Description = "Description du laboratoire inexistant.",
                Id_Adresse_Campus = 1,
                Id_Adresse_Labo = 1,
                Pole_Expertises = new List<int> { 1 },
                mot_Clefs = new List<int> { 1 },
                Thematiques = new List<int> { 1 }
            };
            var action = await _controller.PutLaboratoire("LaboInexistant", dto);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
        }

        [TestMethod]
        public async Task PutLaboratoire_NonExistingId_ReturnBadRequest() 
        {
            LaboratoireUpdateDTO dto = new LaboratoireUpdateDTO
            {
                Nom_Court = "LaboDifferent",
                Nom_Long = "Laboratoire Inexistant",
                Description = "Description du laboratoire inexistant.",
                Id_Adresse_Campus = 1,
                Id_Adresse_Labo = 1,
                Pole_Expertises = new List<int> { 1 },
                mot_Clefs = new List<int> { 1 },
                Thematiques = new List<int> { 1 }
            };
            var action = await _controller.PutLaboratoire("LaboInexistant", dto);
            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestResult");
        }
    }
}
