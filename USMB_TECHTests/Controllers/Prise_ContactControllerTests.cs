using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Prise_ContactControllerTests
    {
        private UsmbTechDbContext _context;
        private Prise_ContactManager _manager;
        private Prise_ContactsController _controller;


        private Prise_Contact _contact1;
        private Prise_Contact _contact2;
        private Prise_Contact _contact3;

        [TestInitialize]
        public void Initialize()
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new UsmbTechDbContext(option);
            _manager = new Prise_ContactManager(_context);
            _controller = new Prise_ContactsController(_manager);

            var equipement = new Equipement { Id_Equipement = 1, Nom_Equipement = "EquipementTest", Description_Technique = "DescrEquipement", Num_Immobilisation = "015458-47re-85" };
            var typeClient = new Type_Client { Id_Type_Client = 1, Nom_Type_Client = "ClientTest" };

            _context.Equipements.Add(equipement);
            _context.Type_Clients.Add(typeClient);

            _contact1 = new Prise_Contact
            {
                Nom_Contact = "Ncontact1",
                Prenom_Contact = "Pcontact1",
                Entreprise_Contact = "entreprise1",
                Email_Contact = "contact1@gmail.com",
                Description_besoins = "description_contact1",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };

            _contact2 = new Prise_Contact
            {
                Nom_Contact = "Ncontact2",
                Prenom_Contact = "Pcontact2",
                Entreprise_Contact = "entreprise2",
                Email_Contact = "contact2@gmail.com",
                Description_besoins = "description_contact2",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };

            _contact3 = new Prise_Contact
            {
                Nom_Contact = "Ncontact3",
                Prenom_Contact = "Pcontact3",
                Entreprise_Contact = "entreprise3",
                Email_Contact = "contact3@gmail.com",
                Description_besoins = "description_contact3",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };

            _context.Prise_Contacts.AddRange(_contact1, _contact2, _contact3);

            _context.SaveChanges();
        }


        [TestMethod]
        public async Task GetAllPrise_Contacts_Return_Ok()
        {
            var action = await _controller.GetPrise_Contacts();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Prise_Contact>;
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Prise_Contact>));
            Assert.AreEqual(3, returnedList.Count(), "Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(c => c.Nom_Contact == "Ncontact1"), "la prise de contact1 est absente");
            CollectionAssert.AreEquivalent(
                _context.Prise_Contacts.Select(c => c.Nom_Contact).ToList(),
                returnedList.Select(c => c.Nom_Contact).ToList(), "Les prises de contacts retournées sont incorrectes");
        }

        [TestMethod]
        public async Task GetPriseById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetPrise_Contact(_contact2.Num_Prise_Contact);
            var okResult = action.Result as OkObjectResult;
            var returnedContact = okResult.Value as Prise_Contact;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedContact, "Le contact retourné est null");
            Assert.IsInstanceOfType(returnedContact, typeof(Prise_Contact), "Le contact retourné n'est pas de type Prise_Contact");
            Assert.AreEqual(_contact2.Nom_Contact, returnedContact.Nom_Contact, "Le nom du contact retourné est incorrect");
        }

        [TestMethod]
        public async Task GetPriseById_NonExistingId_Return_NotFound()
        {
            var action = await _controller.GetPrise_Contact(5);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(action.Value, "Un contact a été trouvé");
        }

        [TestMethod]
        public async Task DeletePrise_Contact_Return_NoContent()
        {
            var action = _controller.DeletePrise_Contact(_contact3.Num_Prise_Contact);
            var contactInDb = await _context.Prise_Contacts.FindAsync(_contact3.Num_Prise_Contact);

            Assert.IsInstanceOfType(action.Result, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(contactInDb, "Le contact n'a pas été supprimé");
        }

        [TestMethod]
        public async Task DeletePrise_Contact_NonExistingId_Return_NotFound()
        {
            var action = await _controller.DeletePrise_Contact(5);
            var contactInDb = await _context.Prise_Contacts.FindAsync(5);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(contactInDb, "Un contact a été trouvé");
        }
        [TestMethod]
        public async Task PostPrise_Contact_Return_CreatedAtAction()
        {
            var action = await _controller.PostPrise_Contact(new Prise_Contact
            {
                Nom_Contact = "Ncontact3",
                Prenom_Contact = "Pcontact3",
                Entreprise_Contact = "entreprise3",
                Email_Contact = "contact3@gmail.com",
                Description_besoins = "description_contact3",
                Id_Equipement = 1,
                Id_Type_Client = 1
            });
            var foundContact = _context.Prise_Contacts.FirstOrDefault(c => c.Email_Contact == "contact3@gmail.com");

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(foundContact, "Le contact n'a pas été ajouté");
            Assert.IsInstanceOfType(foundContact, typeof(Prise_Contact), "Le type de prise est incorrect");
        }

        [TestMethod]
        public async Task PostPrise_InvalidModelState_ReturnBadRequest()
        {
            var contact = new Prise_Contact
            {
                Num_Prise_Contact = 0,
                Nom_Contact = "Ncontact3",
                Prenom_Contact = "Pcontact3",
                Entreprise_Contact = "entreprise3",
                Email_Contact = "contact3@gmail.com",
                Description_besoins = "description_contact3",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };
            _controller.ModelState.AddModelError("Num_Prise_Contact", "Le numéro de la prised de contact est requis.");

            var action = await _controller.PostPrise_Contact(contact);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "la réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutPrise_Contact_ValidUpdate_ReturnNoContent()
        {
            var updatedContact = new Prise_Contact
            {
                Num_Prise_Contact = _contact1.Num_Prise_Contact,
                Nom_Contact = "Ncontact1Updated",
                Prenom_Contact = "Pcontact1Updated",
                Entreprise_Contact = "entreprise1Updated",
                Email_Contact = "contact1@gmail.com",
                Description_besoins = "description_contact1Updated",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };

            var action = await _controller.PutPrise_Contact(_contact1.Num_Prise_Contact, updatedContact);
            var contactInDb = _context.Prise_Contacts.Find(_contact1.Num_Prise_Contact);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(updatedContact.Nom_Contact, contactInDb.Nom_Contact, "Le nom n'a pas été mis à jour");
        }

        [TestMethod]
        public async Task PutPrise_InvalidId_returnBadRequest()
        {
            var updatedContact = new Prise_Contact
            {
                Num_Prise_Contact = _contact1.Num_Prise_Contact,
                Nom_Contact = "Ncontact1Updated",
                Prenom_Contact = "Pcontact1Updated",
                Entreprise_Contact = "entreprise1Updated",
                Email_Contact = "contact1@gmail.com",
                Description_besoins = "description_contact1Updated",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };
            var action = await _controller.PutPrise_Contact(5, updatedContact);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutPrise_Contact_NonExistingId_ReturnNotFound()
        {
            var updatedContact = new Prise_Contact
            {
                Num_Prise_Contact = 5,
                Nom_Contact = "Ncontact5",
                Prenom_Contact = "Pcontact5",
                Entreprise_Contact = "entreprise5",
                Email_Contact = "contact5@gmail.com",
                Description_besoins = "description_contact5",
                Id_Equipement = 1,
                Id_Type_Client = 1
            };
            var action = await _controller.PutPrise_Contact(5, updatedContact);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundObjectResult");
        }
    }
}