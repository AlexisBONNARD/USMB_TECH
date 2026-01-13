using Xunit;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using USMB_TECH.Controllers;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using USMB_TECH.Models;
using USMB_TECH.DTO;
using Assert = Xunit.Assert;
namespace USMB_TECHTests.Controllers
{
    public class SearchControllerTests
    {
        private readonly UsmbTechDbContext _context;
        private readonly IMapper _mapper;

        private readonly EquipementManager _equipManager;
        private readonly Pole_ExpertiseManager _poleManager;
        private readonly PrestationManager _prestationManager;
        private readonly LaboratoireManager _laboManager;
        private readonly Domaine_ExcellenceManager _domaineManager;

        private readonly SearchController _controller;

        public SearchControllerTests()
        {
            var options = new DbContextOptionsBuilder<UsmbTechDbContext>()
                .UseInMemoryDatabase("SearchTestDB")
                .Options;

            _context = new UsmbTechDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(EquipementPreviewDTO).Assembly);
            });

            _mapper = config.CreateMapper();

            // HttpClient vide pour les managers qui en ont besoin
            var httpClient = new HttpClient();

            _equipManager = new EquipementManager(_context);
            _poleManager = new Pole_ExpertiseManager(_context, httpClient);
            _prestationManager = new PrestationManager(_context);
            _laboManager = new LaboratoireManager(_context);
            _domaineManager = new Domaine_ExcellenceManager(_context);

            _controller = new SearchController(
                _equipManager,
                _poleManager,
                _prestationManager,
                _laboManager,
                _domaineManager,
                _mapper
            );
        }


        [Fact]
        public async Task GlobalSearch_ReturnsEmpty_WhenQueryIsEmpty()
        {
            var result = await _controller.GlobalSearch("", "motclef");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<GlobalSearchResultDTO>(ok.Value);

            Assert.Empty(dto.Equipements);
            Assert.Empty(dto.PolesExpertise);
            Assert.Empty(dto.Prestations);
            Assert.Empty(dto.Laboratoires);
            Assert.Empty(dto.DomainesExcellence);
        }

        [Fact]
        public async Task GlobalSearch_FindsEquipement_WhenMatching()
        {
            // Insert test data
            var pole = new Pole_Expertise
            {
                Id_Pole_Expertise = 1,
                Nom_Pole_Expertise = "Test Pole",
                Description_Pole_Expertise = "Description du pôle",   // OBLIGATOIRE
            };
            _context.Pole_Expertises.Add(pole);

            var equip = new Equipement
            {
                Id_Equipement = 1,
                Nom_Equipement = "Imprimante 3D",
                Description_Technique = "Machine de prototypage",
                Num_Immobilisation = "TEST-123",
                Prix_Achat = 1000,
                Disponibilite = true,
                Date_Acquisition = DateTime.Now,

                Id_Pole_Expertise = 1,
                Pole_ExpertiseNavigation = pole
            };

            _context.Equipements.Add(equip);

            await _context.SaveChangesAsync();


            // Act
            var result = await _controller.GlobalSearch("imprimante", "texte");

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<GlobalSearchResultDTO>(ok.Value);

            Assert.Single(dto.Equipements);
            Assert.Equal(1, dto.Equipements[0].Id_Equipement);
        }

    }
}
