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
    public class PlateformesController(IMainRepository<Plateforme, int> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Plateforme, int> _dataRepository = dataRepository;

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
        public async Task<ActionResult<Plateforme>> PostPlateforme(AddPlateformeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ Mapper DTO → entité Plateforme
            var plateforme = PlateformeMapper.ToEntity(dto);

            // 2️⃣ Ajouter la plateforme
            await _dataRepository.AddAsync(plateforme);
            int id = plateforme.Id_Plateforme;

            // 3️⃣ Gérer les mots-clés (Specifier)
            var manager = (PlateformeManager)_dataRepository; // cast vers manager concret

            foreach (var mc in dto.MotsCles)
            {
                int motId;
                if (mc.Id_Mot_Clef.HasValue)
                {
                    motId = mc.Id_Mot_Clef.Value;
                }
                else
                {
                    var newMot = new Mot_Clef { Nom_Mot_Clef = mc.Nom_Mot_Clef };
                    await manager.AddEntityAsync(newMot);
                    motId = newMot.Id_Mot_Clef;
                }

                await manager.AddEntityAsync(new Specifier
                {
                    Id_Plateforme = id,
                    Id_Mot_Clef = motId
                });
            }

            // 4️⃣ Gérer les thématiques (Exposer)
            foreach (var t in dto.Thematiques)
            {
                int themaId;
                if (t.Id_Thematique.HasValue)
                {
                    themaId = t.Id_Thematique.Value;
                }
                else
                {
                    var newT = new Thematique
                    {
                        Nom_Thematique = t.Nom_Thematique,
                        Id_Sous_Thematique = t.Id_Sous_Thematique ?? 0
                    };
                    await manager.AddEntityAsync(newT);
                    themaId = newT.Id_Thematique;
                }

                await manager.AddEntityAsync(new Exposer
                {
                    Id_Plateforme = id,
                    Id_Thematique = themaId
                });
            }

            // 5️⃣ Gérer les exemples d’utilisation
            foreach (var e in plateforme.Exemple_Utilisations)
            {
                e.Id_Plateforme = id;
                await manager.AddEntityAsync(e);
            }

            // 6️⃣ Gérer les photos
            foreach (var p in plateforme.Photos)
            {
                p.Id_Plateforme = id;
                await manager.AddEntityAsync(p);
            }

            // 7️⃣ Valider toutes les entités liées
            await manager.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlateforme), new { id = id }, plateforme);
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
    }
}
