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
                Console.WriteLine("mc : " + mc);
                int motId;

                // Vérifier si le mot-clé existe déjà
                var existingMotCle = await manager.GetMotCleByNameAsync(mc.Nom_Mot_Clef);

                if (existingMotCle != null)
                {
                    // Si le mot-clé existe déjà, récupérer son ID
                    motId = existingMotCle.Id_Mot_Clef;
                }
                else
                {
                    // Si le mot-clé n'existe pas, le créer et récupérer son ID généré
                    var newMot = new Mot_Clef { Nom_Mot_Clef = mc.Nom_Mot_Clef };
                    await manager.AddEntityAsync(newMot);  // Cela va insérer et générer un ID
                    motId = newMot.Id_Mot_Clef; // Maintenant, tu peux récupérer l'ID généré
                }

                // Ajouter la relation Specifier
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

                // Vérifier si la thématique existe déjà
                var existingThematique = await manager.GetThematiqueByNameAsync(t.Nom_Thematique);

                if (existingThematique != null)
                {
                    // Si la thématique existe déjà, récupérer son ID
                    themaId = existingThematique.Id_Thematique;
                }
                else
                {
                    // Si la thématique n'existe pas, la créer et récupérer son ID généré
                    var newThematique = new Thematique
                    {
                        Nom_Thematique = t.Nom_Thematique,
                        Id_Sous_Thematique = t.Id_Sous_Thematique ?? 0
                    };
                    await manager.AddEntityAsync(newThematique);
                    themaId = newThematique.Id_Thematique;  // Récupérer l'ID généré
                }

                // Ajouter la relation Exposer
                await manager.AddEntityAsync(new Exposer
                {
                    Id_Plateforme = id,
                    Id_Thematique = themaId
                });
            }

            // 5️⃣ Gérer les équipements
            foreach (var e in dto.Equipements)
            {
                int equipId;

                // Vérifier si l'équipement existe déjà
                var existingEquip = await manager.Get(e.Nom_Equipement);

                if (existingEquip != null)
                {
                    // Si l'équipement existe déjà, récupérer son ID
                    equipId = existingEquip.Id_Equipement;
                }
                else
                {
                    // Si l'équipement n'existe pas, renvoyer vers la page d'ajout
                    // Mettre cette partie en commentaire pour l'instant
                    /*
                    // Redirection vers la page d'ajout d'un équipement
                    return RedirectToAction("AjouterEquipement", "Equipement");
                    */
                    // Ou pour l'instant, on le crée :
                    var newEquip = new Equipement
                    {
                        Nom_Equipement = e.Nom_Equipement,
                        Description = e.Description
                    };
                    await manager.AddEntityAsync(newEquip);
                    equipId = newEquip.Id_Equipement;  // Récupérer l'ID généré
                }

                // Ajouter la relation avec l'équipement
                await manager.AddEntityAsync(new Utiliser
                {
                    Id_Plateforme = id,
                    Id_Equipement = equipId
                });
            }

            // 6️⃣ Gérer les exemples d’utilisation
            foreach (var ex in plateforme.Exemple_Utilisations)
            {
                ex.Id_Plateforme = id;
                await manager.AddEntityAsync(ex);
            }

            // 7️⃣ Gérer les photos
            foreach (var p in plateforme.Photos)
            {
                p.Id_Plateforme = id;
                await manager.AddEntityAsync(p);
            }

            // 8️⃣ Valider toutes les entités liées
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
