using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.DTO;
using USMB_TECH.Mapper;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateformesController(IMainRepository<Plateforme, int> dataRepository, UsmbTechDbContext context) : ControllerBase
    {
        private readonly IMainRepository<Plateforme, int> _dataRepository = dataRepository;
        private readonly UsmbTechDbContext _context = context;

        // GET: api/Plateformes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Plateforme>>> GetPlateformes()
        {
            var plateformes = await _dataRepository.GetAllAsync();
            return Ok(plateformes);
        }

        // GET: api/Plateformes/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Plateforme>> GetPlateforme(int id)
        {
            var plateforme = await _dataRepository.GetByIdAsync(id);
            return plateforme is null ? NotFound() : Ok(plateforme);
        }

        // PUT: api/Plateformes/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPlateforme(int id, Plateforme plateforme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plateforme.Id_Plateforme)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, plateforme);
            return NoContent();
        }
        // POST: api/Plateformes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Plateforme>> PostPlateforme(AddPlateformeDto plateformeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ Mapper le DTO vers l'entité
            var plateforme = PlateformeMapper.ToEntity(plateformeDto);

            // 2️⃣ Ajouter la plateforme et sauvegarder pour générer l'ID
            await _context.Plateformes.AddAsync(plateforme);
            await _context.SaveChangesAsync();
            int idPlateforme = plateforme.Id_Plateforme;

            // 3️⃣ Gérer les mots-clés (Specifier)
            var specifiersToAdd = new List<Specifier>();
            foreach (var mc in plateformeDto.MotsCles)
            {
                var nomMotClef = mc.Nom_Mot_Clef.Trim().ToLower();

                // Chercher si le mot-clé existe déjà
                var existingMotCle = await _context.Mot_Clefs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Nom_Mot_Clef.ToLower() == nomMotClef);

                int motId;

                if (existingMotCle != null)
                {
                    motId = existingMotCle.Id_Mot_Clef;
                }
                else
                {
                    // Créer et sauvegarder immédiatement pour générer l'ID
                    var newMot = new Mot_Clef { Nom_Mot_Clef = nomMotClef };
                    _context.Mot_Clefs.Add(newMot);
                    await _context.SaveChangesAsync(); // IMPORTANT : génère l'ID dans la DB

                    motId = newMot.Id_Mot_Clef;
                }

                // Ajouter le Specifier
                var spec = new Specifier
                {
                    Id_Plateforme = plateforme.Id_Plateforme,
                    Id_Mot_Clef = motId
                };
                _context.Specifiers.Add(spec);
            }

            // Sauvegarder tous les Specifiers
            await _context.SaveChangesAsync();

            // 4️⃣ Gérer les thématiques (Exposer)
            var exposersToAdd = new List<Exposer>();
            foreach (var t in plateformeDto.Thematiques)
            {
                var nomThematique = t.Nom_Thematique.Trim().ToLower();
                var existingThematique = await _context.Thematiques
                    .FirstOrDefaultAsync(m => m.Nom_Thematique.ToLower() == nomThematique);

                int themaId;
                if (existingThematique != null)
                {
                    themaId = existingThematique.Id_Thematique;
                }
                else
                {
                    var newThematique = new Thematique
                    {
                        Nom_Thematique = nomThematique,
                        Id_Sous_Thematique = t.Id_Sous_Thematique ?? 0
                    };
                    await _context.Thematiques.AddAsync(newThematique);
                    await _context.SaveChangesAsync(); // ID généré
                    themaId = newThematique.Id_Thematique;
                }

                exposersToAdd.Add(new Exposer
                {
                    Id_Plateforme = idPlateforme,
                    Id_Thematique = themaId
                });
            }
            await _context.Exposers.AddRangeAsync(exposersToAdd);

            // 5️⃣ Gérer les exemples d’utilisation
            foreach (var ex in plateforme.Exemple_Utilisations)
            {
                ex.Id_Plateforme = idPlateforme;
                await _context.Exemple_Utilisations.AddAsync(ex);
            }

            // 6️⃣ Gérer les photos
            foreach (var p in plateforme.Photos)
            {
                p.Id_Plateforme = idPlateforme;
                await _context.Photos.AddAsync(p);
            }

            // 7️⃣ Sauvegarder toutes les entités liées
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlateforme), new { id = idPlateforme }, plateforme);
        }




        // DELETE: api/Plateformes/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlateforme(int id)
        {
            var laboratoire = await _dataRepository.GetByIdAsync(id);
            if (laboratoire is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(laboratoire);
            return NoContent();
        }

        [HttpGet("GetByNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Plateforme>>> GetByNom(string nom)
        {
            var results = await _dataRepository.GetByKeysAsync(p => p.Nom_Plateforme, nom);
            return Ok(results);
        }
    }
}
