using Microsoft.AspNetCore.Mvc;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.Repository;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly EquipementManager _equipManager;

    public SearchController(EquipementManager equipManager)
    {
        _equipManager = equipManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipementPreviewDTO>>> GlobalSearch(
    [FromQuery] string query,
    [FromQuery] string mode = "motclef")
    {
        query = query.ToLower().Trim();
        var allEquip = await _equipManager.GetAllAsync();

        var results = new List<EquipementPreviewDTO>();

        if (mode.Contains("motclef"))
        {
            var motClefResults = allEquip
                .Where(e => e.Pole_ExpertiseNavigation.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query)))
                .Select(e => ToPreviewDTO(e));

            results.AddRange(motClefResults);
        }

        if (mode.Contains("thematique"))
        {
            var thematiqueResults = allEquip
                .Where(e => e.Pole_ExpertiseNavigation.Exposers.Any(t =>
                    t.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query)))
                .Select(e => ToPreviewDTO(e));

            results.AddRange(thematiqueResults);
        }

        // Supprimer les doublons si un équipement apparaît dans les deux
        results = results
            .GroupBy(e => e.Id_Equipement)
            .Select(g => g.First())
            .ToList();

        return Ok(results);
    }

    private EquipementPreviewDTO ToPreviewDTO(Equipement e) => new EquipementPreviewDTO
    {
        Id_Equipement = e.Id_Equipement,
        Nom_Equipement = e.Nom_Equipement,
        Description_Technique = e.Description_Technique,
        Nom_Pole_Expertise = e.Pole_ExpertiseNavigation?.Nom_Pole_Expertise,
        Prix_Achat = e.Prix_Achat,
        Date_Acquisition = e.Date_Acquisition,
        Disponibilite = e.Disponibilite,
        MotsCles = e.Pole_ExpertiseNavigation?.Specifiers
            .Select(s => s.Mot_ClefNavigation.Nom_Mot_Clef)
            .Distinct()
            .ToList() ?? new List<string>(),
        Thematiques = e.Pole_ExpertiseNavigation?.Exposers
            .Select(t => t.ThematiqueNavigation.Nom_Thematique)
            .Distinct()
            .ToList() ?? new List<string>()
    };

}
