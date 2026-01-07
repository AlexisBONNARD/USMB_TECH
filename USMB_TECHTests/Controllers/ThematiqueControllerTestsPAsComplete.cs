using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECHTests.AutoMapper;

namespace USMB_TECHTests.Controllers
{
    [TestClass]
    [TestCategory("intégration")]
    public class ThematiqueControllerTests : AutoMapperConfigTests
    {
        private UsmbTechDbContext _context;
        private ThematiqueController _controller;
        private ThematiqueManager _manager;

        private Thematique _thematique1;
        private Thematique _thematique2;
        private Thematique _thematique3;

        [TestInitialize]
        public void Initialize()
        {
            var option = new DbContextOptionsBuilder<UsmbTechDbContext>()
               .UseInMemoryDatabase(databaseName: $"UsmbTechTestDb_{Guid.NewGuid()}")
               .Options;
            _context = new UsmbTechDbContext(option);
            _manager = new ThematiqueManager(_context);
            _controller = new ThematiqueController(_manager, _mapper);

            _thematique1 = new Thematique { Nom_Thematique = "Thematique1" };
            _thematique2 = new Thematique { Nom_Thematique = "Thematique2" };
            _thematique3 = new Thematique { Nom_Thematique = "Thematique3" };

            _context.Thematiques.AddRange(_thematique1, _thematique2, _thematique3);
            _context.SaveChanges();
        }

    }
}
