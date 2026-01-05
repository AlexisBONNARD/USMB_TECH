using Microsoft.VisualStudio.TestTools.UnitTesting;
using USMB_TECH.Controllers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECH.Models;
using AutoMapper;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECH.Controllers.Tests
{
    [TestClass()]
    [TestCategory("intégration")]
    public class Type_EquipementsControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private Type_EquipementsController _controller;
        private Type_EquipementManager _manager;

        private Type_Equipement _type1;
        private Type_Equipement _type2;
        private Type_Equipement _type3;

        [TestInitialize]
        public void Initialize() 
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
                .Options;
            _context = new UsmbTechDbContext(option);

            _manager = new Type_EquipementManager(_context);
            _controller = new Type_EquipementsController(_manager);

            _type1 = new Type_Equipement { Nom_Type = "Type1" };
            _type2 = new Type_Equipement { Nom_Type = "Type2" };
            _type3 = new Type_Equipement { Nom_Type = "Type3" };

            _context.Type_Equipements.AddRange(_type1, _type2, _type3);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllTypes()
        {
            var action = await _controller.GetType_Equipements();

            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "la réponse n'est pas OK");

            var okResult = action.Result as OkObjectResult;

            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Type_Equipement>),"Les types d'équipements ne sont pas bien récupérés");

            var returnedList = okResult.Value as IEnumerable<Type_Equipement>;

            Assert.AreEqual(3, returnedList.Count(), "Ils n'y a pas un nombre de trois types ");

            CollectionAssert.AreEquivalent(
                _context.Type_Equipements.Select(t => t.Nom_Type).ToList(),
                returnedList.Select(t => t.Nom_Type).ToList()
            );
        }

    }
}