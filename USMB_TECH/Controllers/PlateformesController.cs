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
        public async Task<IActionResult> PutPlateforme(int id, UpdatePlateformeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id_Plateforme)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Plateforme avec l'id {id} introuvable.");

            // Mapper le DTO vers l'entité
            var updatedEntity = PlateformeMapper.ToEntity(dto);

            await _dataRepository.UpdateAsync(existing, updatedEntity);
            return NoContent();
        }

        // POST: api/Plateformes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Plateforme>> PostPlateforme(AddPlateformeDto plateformeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // On ouvre une transaction pour tout rollback en cas d'erreur
            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // -------------------------
                // 1) Créer la plateforme (manuellement, pas via mapper pour éviter collections pré-remplies)
                // -------------------------
                var plateforme = new Plateforme
                {
                    Nom_Plateforme = plateformeDto.Nom_Plateforme?.Trim(),
                    Description_Plateforme = plateformeDto.Description_Plateforme?.Trim(),
                    Nom_Contenu = plateformeDto.Nom_Contenu?.Trim(),
                    Url_Contenu = plateformeDto.Url_Contenu?.Trim(),
                    Description_Contenu = plateformeDto.Description_Contenu?.Trim(),
                    Actif = plateformeDto.Actif
                };

                _context.Plateformes.Add(plateforme);
                await _context.SaveChangesAsync(); // Génère Id_Plateforme
                var idPlateforme = plateforme.Id_Plateforme;

                // -------------------------
                // 2) Mots-clés -> Mot_Clef + Specifier
                // Pour chaque mot : reuse si existe (bd), sinon créer et SaveChanges pour obtenir l'id
                // -------------------------
                foreach (var mc in plateformeDto.MotsCles ?? Enumerable.Empty<MotCleDto>())
                {
                    var nomMot = (mc.Nom_Mot_Clef ?? string.Empty).Trim().ToLower();
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
                        .AnyAsync(s => s.Id_Plateforme == idPlateforme && s.Id_Mot_Clef == motId);

                    if (!existsSpecifier)
                    {
                        _context.Specifiers.Add(new Specifier
                        {
                            Id_Plateforme = idPlateforme,
                            Id_Mot_Clef = motId
                        });
                    }
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 3) Thématiques -> Thematique + Exposer
                // -------------------------
                foreach (var t in plateformeDto.Thematiques ?? Enumerable.Empty<ThematiqueDto>())
                {
                    var nomT = (t.Nom_Thematique ?? string.Empty).Trim().ToLower();
                    if (string.IsNullOrEmpty(nomT)) continue;

                    var existingT = await _context.Thematiques
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Nom_Thematique.ToLower() == nomT);

                    int themeId;
                    if (existingT != null)
                    {
                        themeId = existingT.Id_Thematique;
                    }
                    else
                    {
                        var localT = _context.Thematiques.Local
                            .FirstOrDefault(x => string.Equals(x.Nom_Thematique, nomT, StringComparison.OrdinalIgnoreCase));

                        if (localT != null && localT.Id_Thematique > 0)
                        {
                            themeId = localT.Id_Thematique;
                        }
                        else if (localT != null && localT.Id_Thematique == 0)
                        {
                            await _context.SaveChangesAsync();
                            themeId = localT.Id_Thematique;
                        }
                        else
                        {
                            var newT = new Thematique
                            {
                                Nom_Thematique = nomT,
                                Id_Sous_Thematique = t.Id_Sous_Thematique ?? 0
                            };
                            _context.Thematiques.Add(newT);
                            await _context.SaveChangesAsync();
                            themeId = newT.Id_Thematique;
                        }
                    }

                    // Eviter doublon d'association
                    var existsExposer = await _context.Exposers
                        .AnyAsync(e => e.Id_Plateforme == idPlateforme && e.Id_Thematique == themeId);

                    if (!existsExposer)
                    {
                        _context.Exposers.Add(new Exposer
                        {
                            Id_Plateforme = idPlateforme,
                            Id_Thematique = themeId
                        });
                    }
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 4) Exemples d'utilisation
                // -------------------------
                foreach (var exDto in plateformeDto.ExempleUtilisations ?? Enumerable.Empty<ExempleUtilisationDto>())
                {
                    var newEx = new Exemple_Utilisation
                    {
                        Nom_Utilisation = exDto.Nom_Utilisation?.Trim(),
                        Description_Utilisation = exDto.Description_Utilisation?.Trim(),
                        Id_Plateforme = idPlateforme,
                        Id_Equipement = null // car ton DTO n’a pas ce champ
                    };

                    _context.Exemple_Utilisations.Add(newEx);
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 5) Photos
                // -------------------------
                foreach (var pDto in plateformeDto.Photos ?? Enumerable.Empty<PhotoDto>())
                {
                    var newP = new Photo
                    {
                        Nom_Photo = pDto.Nom_Photo?.Trim(),
                        Url_Photo = pDto.Url_Photo?.Trim(),
                        Id_Plateforme = idPlateforme
                    };

                    _context.Photos.Add(newP);
                }

                await _context.SaveChangesAsync();

                // -------------------------
                // 6) Presenter
                // -------------------------
                foreach (var presenter in plateformeDto.Presenters ?? Enumerable.Empty<Presenter>())
                {
                    var newPresenter = new Presenter
                    {
                        Id_Plateforme = idPlateforme,
                        Id_Prestation = presenter.Id_Prestation
                    };

                    _context.Presenters.Add(newPresenter);
                }

                await _context.SaveChangesAsync();

                // Commit transaction
                await tx.CommitAsync();

                // Recharger la plateforme (optionnel) pour retourner l'objet complet
                await _context.Entry(plateforme).ReloadAsync();

                return CreatedAtAction(nameof(GetPlateforme), new { id = idPlateforme }, plateforme);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                // Tu peux logger ex.Message / ex.ToString() ici
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
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
