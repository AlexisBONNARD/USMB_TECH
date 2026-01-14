using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using USMB_TECH.Controllers;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class Contact_UsmbControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Contact_USMBController _controller;
        private Contact_USMBManager _manager;

        private Contact_USMB _contact1;
        private Contact_USMB _contact2;
        private Contact_USMB _contact3;

        private Fonction fonction;
        private Adresse adresse;
        private Laboratoire labo;

        private AddContactDTO _addContactDto1;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(options);
            _manager = new Contact_USMBManager(_context);
            _controller = new Contact_USMBController(_manager, _mapper);

            fonction = new Fonction { Id_Fonction = 1, Nom_Fonction = "Responsable" };
            adresse = new Adresse { Id_Adresse = 1, Rue_Adresse = "1 rue de l'université", Ville_Adresse = "Chambéry", Code_Postal_Adresse = "73000", Pays_Adresse = "France" };
            labo = new Laboratoire { Nom_Court = "Labo1", Id_Adresse_Campus = 1, Id_Adresse_Labo = 1, Nom_Long = "Laboratoire 1", Description = "Description du labo 1" };

            _context.Fonctions.Add(fonction);
            _context.Adresses.Add(adresse);
            _context.Laboratoires.Add(labo);

            _contact1 = new Contact_USMB
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

            _contact2 = new Contact_USMB
            {
                Prenom_Contact = "Jane",
                Nom_Contact = "Doe",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact2@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            _contact3 = new Contact_USMB
            {
                Prenom_Contact = "Burnice",
                Nom_Contact = "Stars",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact3@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            _addContactDto1 = new AddContactDTO
            {
                Prenom_Contact = "Ellen",
                Nom_Contact = "Joe",
                Code_RH = "JE123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Fonction = "Responsable",
                Nom_Court = labo.Nom_Court,
            };

            _context.Contact_USMBs.AddRange(_contact1, _contact2, _contact3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllContact_Return_Ok()
        {
            var action = await _controller.GetContact_USMBs();
            var okResult = action.Result as OkObjectResult;
            var returnedList = okResult.Value as IEnumerable<Contact_USMB>;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Contact_USMB>), "la valeur retournée n'est pas de type IEnumerable<Contact_USMB>");
            Assert.AreEqual(3, returnedList.Count(), "Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(c => c.Mail == _contact1.Mail), "Le contact attendu n'a pas été trouvé dans la liste retournée");
            CollectionAssert.AreEquivalent(_context.Contact_USMBs.Select(c => c.Nom_Contact).ToList(),
                returnedList.Select(c => c.Nom_Contact).ToList(), "Les contacts retournés sont incorrects");
        }

        [TestMethod]
        public async Task GetContactById_ExistingId_Return_Ok()
        {
            var action = await _controller.GetContact_USMB(_contact1.Id_Contact);
            var okResult = action.Result as OkObjectResult;
            var returnedContact = okResult.Value as Contact_USMB;

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas de type OkObjectResult");
            Assert.IsNotNull(returnedContact, "le contact retourné est null");
            Assert.IsInstanceOfType(returnedContact, typeof(Contact_USMB), "la valeur retournée n'est pas de type Contact_USMB");
            Assert.AreEqual(_contact1.Nom_Contact, returnedContact.Nom_Contact, "Le nom du contact retourné est incorrect");
        }

        [TestMethod]
        public async Task GetContactById_NonExistingId_Return_NotFound()
        {
            var action = await _controller.GetContact_USMB(999);

            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(action.Value, "Un contact a été trouvé");
        }

        [TestMethod]
        public async Task DeleteContact_Return_NoContent()
        {
            var action = await _controller.DeleteContact_USMB(_contact2.Id_Contact);
            var contactInDb = await _context.Contact_USMBs.FindAsync(_contact2.Id_Contact);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.IsNull(contactInDb, "Le contact n'a pas été supprimé de la base de données");
        }

        [TestMethod]
        public async Task DeleteContact_NonExistingId_Return_NotFound()
        {
            var action = await _controller.DeleteContact_USMB(999);
            var contactInDb = await _context.Contact_USMBs.FindAsync(999);
            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
            Assert.IsNull(contactInDb, "Un contact a été trouvé");
        }

        [TestMethod]
        public async Task PostContact_Return_CreatedAtAction()
        {
            var action = await _controller.PostContact_USMB(_addContactDto1);
            var contactInDb = await _context.Contact_USMBs
                .FirstOrDefaultAsync(c => c.Mail == _addContactDto1.Mail);

            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "la réponse n'est pas de type CreatedAtActionResult");
            Assert.IsNotNull(contactInDb, "Le contact n'a pas été ajouté à la base de données");
            Assert.IsInstanceOfType(contactInDb, typeof(Contact_USMB), "le contact ajouté n'est pas de type Contact_USMB");
        }

        [TestMethod]
        public async Task PostPrise_InvalidModelState_ReturnBadRequest()
        {
            AddContactDTO contact = new AddContactDTO
            {
                Prenom_Contact = "Ellen",
                Nom_Contact = "Joe",
                Code_RH = "JE123",
                Num_Securite_Social = "20210039936",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Fonction = "Responsable",
                Nom_Court = labo.Nom_Court,
            };

            _controller.ModelState.AddModelError("Num_Securite_Social", "Le numéro de sécurité sociale est invalide.");

            var action = await _controller.PostContact_USMB(contact);
            var result = action.Result as BadRequestObjectResult;

            Assert.IsNotNull(result, "La réponse est nulle");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObjectResult");
        }

        [TestMethod]
        public async Task PutContact_ValidUpdate_ReturnNoContent()
        {
            var updatedContact = new Contact_USMB
            {
                Id_Contact = 1,
                Prenom_Contact = "EllenUpdated",
                Nom_Contact = "JoeUpdated",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            var action = await _controller.PutContact_USMB(_contact1.Id_Contact, updatedContact);
            var contactInDb = _context.Contact_USMBs.Find(_contact1.Id_Contact);

            Assert.IsInstanceOfType(action, typeof(NoContentResult), "la réponse n'est pas de type NoContentResult");
            Assert.AreEqual(updatedContact.Nom_Contact, contactInDb.Nom_Contact, "Le contact n'a pas été mis à jour correctement");
        }

        [TestMethod]
        public async Task PutContact_NonExistingID_ReturnBadRequestResult()
        {
            var updatedContact = new Contact_USMB
            {
                Id_Contact = _contact1.Id_Contact,
                Prenom_Contact = "EllenUpdated",
                Nom_Contact = "JoeUpdated",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            var action = await _controller.PutContact_USMB(150, updatedContact);

            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult), "la réponse n'est pas de type BadRequestObectResult");
        }

        [TestMethod]
        public async Task PutContact_NonExistingId_ReturnNotFound()
        {
            var updatedContact = new Contact_USMB
            {
                Id_Contact = 150,
                Prenom_Contact = "EllenUpdated",
                Nom_Contact = "JoeUpdated",
                Code_RH = "EJ123",
                Num_Securite_Social = "202100399200136",
                Mail = "contact1@gmail.com",
                Telephone = "0612345678",
                Nom_Court = labo.Nom_Court,
                Id_Fonction = fonction.Id_Fonction,
            };

            var action = await _controller.PutContact_USMB(150, updatedContact);

            Assert.IsInstanceOfType(action, typeof(NotFoundObjectResult), "la réponse n'est pas de type NotFoundResult");
        }
    }
}
