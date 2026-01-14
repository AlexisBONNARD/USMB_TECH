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
    public class Pole_ExpertisesController(IMainRepository<Pole_Expertise, int> dataRepository, UsmbTechDbContext context) : ControllerBase
    {
        private readonly IMainRepository<Pole_Expertise, int> _dataRepository = dataRepository;
        private readonly UsmbTechDbContext _context = context;

        // GET: api/Pole_Expertises
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pole_Expertise>>> GetPole_Expertises()
        {
            var pole_expertises = await _dataRepository.GetAllAsync();
            return Ok(pole_expertises);
        }

        // GET: api/Pole_Expertises/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pole_Expertise>> GetPole_Expertise(int id)
        {
            var pole_expertise = await _dataRepository.GetByIdAsync(id);
            return pole_expertise is null ? NotFound() : Ok(pole_expertise);
        }

        // PUT: api/Pole_Expertises/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPole_Expertise(int id, UpdatePole_ExpertiseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id_Pole_Expertise)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Pole_Expertise avec l'id {id} introuvable.");

            // Mapper le DTO vers l'entité
            var updatedEntity = Pole_ExpertiseMapper.ToEntity(dto);

            await _dataRepository.UpdateAsync(existing, updatedEntity);
            return NoContent();
        }

        // POST: api/Pole_Expertises
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Pole_Expertise>> PostPole_Expertise(AddPole_ExpertiseDto pole_expertiseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // On ouvre une transaction pour tout rollback en cas d'erreur
            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // -------------------------
                // 1) Créer la pole_expertise (manuellement, pas via mapper pour éviter collections pré-remplies)
                // -------------------------
                var pole_expertise = new Pole_Expertise
                {
                    Nom_Pole_Expertise = pole_expertiseDto.Nom_Pole_Expertise?.Trim(),
                    Description_Pole_Expertise = pole_expertiseDto.Description_Pole_Expertise?.Trim(),
                    Actif = pole_expertiseDto.Actif,
                    Id_Domaine_Excellence = pole_expertiseDto.Id_Domaine_Excellence
                };

                _context.Pole_Expertises.Add(pole_expertise);
                await _context.SaveChangesAsync(); 
                var idPole_Expertise = pole_expertise.Id_Pole_Expertise;

                // -------------------------
                // 2) Mots-clés -> Mot_Clef + Specifier
                // Pour chaque mot : reuse si existe (bd), sinon créer et SaveChanges pour obtenir l'id
                // -------------------------
                foreach (var mc in pole_expertiseDto.MotsCles ?? Enumerable.Empty<string>())
                {
                    var nomMot = (mc ?? string.Empty).Trim().ToLower();
                    if (string.IsNullOrEmpty(nomMot)) continue;

                    // Cherche en base (no tracking)
                    var existingMot = await _context.Mot_Clefs
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Nom_Mot_Clef.ToLower() == nomMot);

                    int motId;
                    if (existingMot != null)
                    {
                        motId = existingMot.Id_Mot_Clef;
                    }
                    else
                    {
                        // Vérifier dans le Local (au cas où on aurait déjà ajouté ce mot dans cette transaction)
                        var localMot = _context.Mot_Clefs.Local
                            .FirstOrDefault(m => string.Equals(m.Nom_Mot_Clef, nomMot, StringComparison.OrdinalIgnoreCase));

                        if (localMot != null && localMot.Id_Mot_Clef > 0)
                        {
                            motId = localMot.Id_Mot_Clef;
                        }
                        else if (localMot != null && localMot.Id_Mot_Clef == 0)
                        {
                            // Si local présent mais id=0 (rare), persistons immédiatement
                            await _context.SaveChangesAsync();
                            motId = localMot.Id_Mot_Clef;
                        }
                        else
                        {
                            var newMot = new Mot_Clef { Nom_Mot_Clef = nomMot };
                            _context.Mot_Clefs.Add(newMot);
                            await _context.SaveChangesAsync(); // IMPORTANT pour récupérer Id_Mot_Clef
                            motId = newMot.Id_Mot_Clef;
                        }
                    }

                    // Ajouter la relation Specifier
                    // Eviter doublon : on peut vérifier s'il existe déjà la relation (optionnel)
                    var existsSpecifier = await _context.Specifiers
                        .AnyAsync(s => s.Id_Pole_Expertise == idPole_Expertise && s.Id_Mot_Clef == motId);

                    if (!existsSpecifier)
                    {
                        _context.Specifiers.Add(new Specifier
                        {
                            Id_Pole_Expertise = idPole_Expertise,
                            Id_Mot_Clef = motId
                        });
                    }
                }

                await _context.SaveChangesAsync();


                // -------------------------
                // 4) Exemples d'utilisation
                // -------------------------
                foreach (var exDto in pole_expertiseDto.ExempleUtilisations ?? Enumerable.Empty<Exemple_Utilisation>())
                {
                    var newEx = new Exemple_Utilisation
                    {
                        Nom_Utilisation = exDto.Nom_Utilisation?.Trim(),
                        Description_Utilisation = exDto.Description_Utilisation?.Trim(),
                        Id_Pole_Expertise = idPole_Expertise,
                        Id_Equipement = null // car ton DTO n’a pas ce champ
                    };

                    _context.Exemple_Utilisations.Add(newEx);
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 5) Photos
                // -------------------------
                foreach (var pDto in pole_expertiseDto.Photos ?? Enumerable.Empty<Photo>())
                {
                    var newP = new Photo
                    {
                        Nom_Photo = pDto.Nom_Photo?.Trim(),
                        Url_Photo = pDto.Url_Photo?.Trim(),
                        Id_Pole_Expertise = idPole_Expertise
                    };

                    _context.Photos.Add(newP);
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 6) Presenter
                // -------------------------
                foreach (var presenter in pole_expertiseDto.Presenters ?? Enumerable.Empty<Presenter>())
                {
                    var newPresenter = new Presenter
                    {
                        Id_Pole_Expertise = idPole_Expertise,
                        Id_Prestation = presenter.Id_Prestation
                    };

                    _context.Presenters.Add(newPresenter);
                }

                await _context.SaveChangesAsync();

                // Commit transaction
                await tx.CommitAsync();

                // Recharger la pole_expertise (optionnel) pour retourner l'objet complet
                await _context.Entry(pole_expertise).ReloadAsync();

                return CreatedAtAction(nameof(GetPole_Expertise), new { id = idPole_Expertise }, pole_expertise);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                // Tu peux logger ex.Message / ex.ToString() ici
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        // DELETE: api/Pole_Expertises/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePole_Expertise(int id)
        {
            var pole_expertise = await _dataRepository.GetByIdAsync(id);
            if (pole_expertise is null)
                return NotFound($"Pole_Expertise avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(pole_expertise);
            return NoContent();
        }

        [HttpGet("GetByNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Pole_Expertise>>> GetByNom(string nom)
        {
            var results = await _dataRepository.GetByKeysAsync(p => p.Nom_Pole_Expertise, nom);
            return Ok(results);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Aucun fichier reçu");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "../USMB_TECH_Blazor/wwwroot/uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { url = $"/uploads/{file.FileName}" });
        }

    }
}
