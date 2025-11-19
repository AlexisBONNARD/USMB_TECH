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

            var plateforme = PlateformeMapper.ToEntity(plateformeDto);

            await _dataRepository.AddAsync(plateforme);
            int idPlateforme = plateforme.Id_Plateforme;

            // 3️⃣ Gérer les mots-clés (Specifier)
            foreach (var mc in plateformeDto.MotsCles)
            {
                int motId;

                var nomMotClef = mc.Nom_Mot_Clef.Trim().ToLower();

                // Récupérer l'entité si elle existe
                var existingMotCle = await _context.Mot_Clefs.FirstOrDefaultAsync(m => m.Nom_Mot_Clef.ToLower() == nomMotClef);

                if (existingMotCle != null)
                {
                    motId = existingMotCle.Id_Mot_Clef;
                }
                else
                {
                    // Créer le mot-clé
                    var newMot = new Mot_Clef { Nom_Mot_Clef = nomMotClef };
                    await _context.Mot_Clefs.AddAsync(newMot);
                    await _context.SaveChangesAsync(); // Nécessaire pour générer l'ID

                    motId = newMot.Id_Mot_Clef;
                }
                Console.WriteLine($"Mot-clé traité : {nomMotClef} avec ID {motId}");
                Console.WriteLine($"Plateforme ID : {idPlateforme}");
                // Ajouter la relation dans Specifier
                await _context.Specifiers.AddAsync(new Specifier
                {
                    Id_Plateforme = idPlateforme,
                    Id_Mot_Clef = motId
                });
            }

            // Sauvegarde finale
            await _context.SaveChangesAsync();


            // 4️⃣ Gérer les thématiques (Exposer)
            foreach (var t in plateformeDto.Thematiques)
            {
                int themaId;

                var nomThematique = t.Nom_Thematique.Trim().ToLower();

                // Vérifier si la thématique existe déjà
                var existingThematique = await _context.Thematiques.FirstOrDefaultAsync(m => m.Nom_Thematique.ToLower() == nomThematique);

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
                        Nom_Thematique = nomThematique,
                        Id_Sous_Thematique = t.Id_Sous_Thematique ?? 0
                    };
                    await _context.Thematiques.AddAsync(newThematique);
                    themaId = newThematique.Id_Thematique;  // Récupérer l'ID généré
                }

                // Ajouter la relation Exposer
                await _context.Exposers.AddAsync(new Exposer
                {
                    Id_Plateforme = idPlateforme,
                    Id_Thematique = themaId
                });
            }

            // 5️⃣ Gérer les équipements
            foreach (var e in plateformeDto.Equipements)
            {
                int equipId;

                var nomEquipement = e.Nom_Equipement.Trim().ToLower();

                // Vérifier si l'équipement existe déjà
                var existingEquip = await _context.Equipements.FirstOrDefaultAsync(m => m.Nom_Equipement.ToLower() == nomEquipement);

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
                    return BadRequest($"L'équipement '{nomEquipement}' n'existe pas. Veuillez l'ajouter avant de l'associer à une plateforme.");
                }
            }

            // 6️⃣ Gérer les exemples d’utilisation
            foreach (var ex in plateforme.Exemple_Utilisations)
            {
                ex.Id_Plateforme = idPlateforme;
                await _context.Exemple_Utilisations.AddAsync(ex);
            }

            // 7️⃣ Gérer les photos
            foreach (var p in plateforme.Photos)
            {
                p.Id_Plateforme = idPlateforme;
                await _context.Photos.AddAsync(p);
            }

            // 8️⃣ Valider toutes les entités liées
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
