using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class PrestationControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private PrestationsController _controller;
        private PrestationManager _manager;

        private Prestation _prestation1;
        private Prestation _prestation2;
        private Prestation _prestation3;

        private AddPrestationDTO _addPrestationDTO;

        private Contact_USMB contact;
        private Type_Prestation type;
        private Domaine_Excellence domaine;
        private Unite_Oeuvre unite;
        private Laboratoire labo;
        private Fonction fonction;
        private Adresse adresse;

        [TestInitialize]
        public void Initialize() 
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(options);
            _manager = new PrestationManager(_context);
            _controller = new PrestationsController(_manager, _mapper);

            domaine = new Domaine_Excellence { Id_Domaine_Excellence = 1, intitule_Domaine_Excellence = "Domaine 1" };
            unite = new Unite_Oeuvre { Id_Unite_Oeuvre = 1, Nom_Unite_Oeuvre = "Heure"};
            fonction = new Fonction { Id_Fonction = 1, Nom_Fonction = "Responsable" };
            type = new Type_Prestation { Id_Type_Prestation = 1, Nom_Type_Prestation = "Type 1" };
            adresse = new Adresse { Id_Adresse = 1, Rue_Adresse = "1 rue de l'université", Ville_Adresse = "Chambéry", Code_Postal_Adresse = "73000", Pays_Adresse = "France" };
            labo = new Laboratoire { Nom_Court = "Labo1", Id_Adresse_Campus = 1, Id_Adresse_Labo = 1, Nom_Long = "Laboratoire 1", Description = "Description du labo 1" };
            contact = new Contact_USMB
            {
                Prenom_Contact = "Ellen",
                Nom_Contact = "Joe",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            _context.Laboratoires.Add(labo);
            _context.Domaine_Excellences.Add(domaine);
            _context.Unite_Oeuvres.Add(unite);
            _context.Fonctions.Add(fonction);
            _context.Type_Prestations.Add(type);
            _context.Adresses.Add(adresse);
            _context.Contact_USMBs.Add(contact);

            _prestation1 = new Prestation 
            {
                Intitule_Prestation = "Prestation 1",
                Description_Prestation = "Description 1",
                Id_Unite_Oeuvre = 1,
                Id_Type_Prestation = 1,
                Id_Domaine_Excellence = 1,
                Id_Contact = 1,
            };

            _prestation2 = new Prestation
            {
                Intitule_Prestation = "Prestation 2",
                Description_Prestation = "Description 2",
                Id_Unite_Oeuvre = 1,
                Id_Type_Prestation = 1,
                Id_Domaine_Excellence = 1,
                Id_Contact = 1,
            };

            _prestation3 = new Prestation
            {
                Intitule_Prestation = "Prestation 3",
                Description_Prestation = "Description 3",
                Id_Unite_Oeuvre = 1,
                Id_Type_Prestation = 1,
                Id_Domaine_Excellence = 1,
                Id_Contact = 1,
            };

            _addPrestationDTO = new AddPrestationDTO
            {
                Intitule_Prestation = "PrestationDTO",
                Description_Prestation = "Description PrestationDTO",
                Nom_Court = labo.Nom_Court,
                Nom_Contact = contact.Nom_Contact,
                Nom_Domaine = domaine.intitule_Domaine_Excellence,
                Type = type.Nom_Type_Prestation,
                Unite = unite.Nom_Unite_Oeuvre
            };

            _context.AddRange(_prestation1, _prestation2, _prestation3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllPrestation_Return_Ok() 
        {
            var action = await _controller.GetPrestations();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Prestation>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Prestation>));
            Assert.AreEqual(3, returnedList.Count(),"Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(p => p.Intitule_Prestation == _prestation1.Intitule_Prestation), "La prestation n'a pas été trouvée dans la liste retournée");
            CollectionAssert.AreEquivalent(_context.Prestations.Select(p => p.Intitule_Prestation).ToList(), 
                returnedList.Select(p => p.Intitule_Prestation).ToList(), "Les prestations retournées sont incorrects");
        }

        [TestMethod]
        public async Task GetPrestationById_Existing_Return_Ok() 
        {
            var action = await _controller.GetPrestation(_prestation1.Id_Prestation);
            var okResult = action.Result as OkObjectResult;
            var returnedPrestation = okResult.Value as Prestation;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult),"La réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedPrestation,"la prestation retourné est null");
            Assert.IsInstanceOfType(returnedPrestation, typeof(Prestation), "la valeur retournée n'est pas de type Prestation");
            Assert.AreEqual(_prestation1.Intitule_Prestation, returnedPrestation.Intitule_Prestation, "l'intitulé de la prestation retourné est incorrect");
        }

        [TestMethod]
        public async Task GetPrestationById_NonExistingId_Return_NotFound() 
        {
            var action = await _controller.GetPrestation(11037);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(action.Value, "Une prestation a été trouvée");
        }

        [TestMethod]
        public async Task DeletePrestation_Return_NoContent()
        {
            var action = await _controller.DeletePrestation(_prestation1.Id_Prestation);
            var prestationInDb = await _context.Prestations.FindAsync(_prestation1.Id_Prestation);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(prestationInDb);
        }

        [TestMethod]
        public async Task DeletePrestation_NonExistingId_Return_NotFound() 
        {
            var action = await _controller.DeletePrestation(999);
            var prestationIdDb = await _context.Contact_USMBs.FindAsync(999);

            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
            Assert.IsNull(prestationIdDb, "une prestation a été trouvée");
        }

        [TestMethod]
        public async Task PostPrestation_Return_CreatedAtAction() 
        {
            var action = await _controller.PostPrestation(_addPrestationDTO);
            var prestationInDb = await _context.Prestations
                .FirstOrDefaultAsync(p => p.Intitule_Prestation == _addPrestationDTO.Intitule_Prestation);

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(prestationInDb, "la prestation n'a pas été ajoutée à la base");
            Assert.IsInstanceOfType(prestationInDb, typeof(Prestation), "la prestation ajouté n'est pas de type Prestation");
        }

        [TestMethod]
        public async Task PostPrestation_InvalidModelState_RetrnBadRequest() 
        {
            AddPrestationDTO addPrestationDTO = new AddPrestationDTO
            {
                Id_Prestation = 0,
                Intitule_Prestation = "PrestationDTO",
                Description_Prestation = "Description PrestationDTO",
                Nom_Court = labo.Nom_Court,
                Nom_Contact = contact.Nom_Contact,
                Nom_Domaine = domaine.intitule_Domaine_Excellence,
                Type = type.Nom_Type_Prestation,
                Unite = unite.Nom_Unite_Oeuvre
            };

            _controller.ModelState.AddModelError("Id_Prestation", "le numéro de prestation est invalide");

            var action = await _controller.PostPrestation(addPrestationDTO);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        }
    }
}
