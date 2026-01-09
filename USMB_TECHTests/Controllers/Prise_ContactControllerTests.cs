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

            // 1️⃣ Ajout des entités liées
            var equipement = new Equipement { Id_Equipement = 1, Nom_Equipement = "EquipementTest", Description_Technique="DescrEquipement", Num_Immobilisation="015458-47re-85"};
            var pole = new Pole_Expertise { Id_Pole_Expertise = 1, Nom_Pole_Expertise = "PoleTest", Description_Contenu = "DescrContenu", Description_Pole_Expertise = "DescrPole", Nom_Contenu = "NomContenu",Url_Contenu="https://url.com" };
            var typeClient = new Type_Client { Id_Type_Client = 1, Nom_Type_Client = "ClientTest" };

            _context.Equipements.Add(equipement);
            _context.Pole_Expertises.Add(pole);
            _context.Type_Clients.Add(typeClient);

            _contact1 = new Prise_Contact
            {
                Nom_Contact = "Ncontact1",
                Prenom_Contact = "Pcontact1",
                Entreprise_Contact = "entreprise1",
                Email_Contact = "contact1@gmail.com",
                Description_besoins = "description_contact1",
                Id_Equipement = 1,
                Id_Pole_Expertise = 1,
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
                Id_Pole_Expertise = 1,
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
                Id_Pole_Expertise = 1,
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
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult),"la réponse n'est pas de type OkObjectResult");
            Assert.IsInstanceOfType(returnedList, typeof(IEnumerable<Prise_Contact>));
            Assert.AreEqual(3, returnedList.Count(), "Le nombre d'éléments est incorrect");
            Assert.IsTrue(returnedList.Any(c => c.Nom_Contact == "Ncontact1"), "la prise de contact1 est absente");
            CollectionAssert.AreEquivalent(
                _context.Prise_Contacts.Select(c => c.Nom_Contact).ToList(),
                returnedList.Select(c => c.Nom_Contact).ToList(), "Les prises de contacts retournées sont incorrectes");
        }
    }
}
