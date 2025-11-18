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
    public async Task<ActionResult<IEnumerable<EquipementPreviewDTO>>> GlobalSearch([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Query is required.");

        query = query.ToLower();

        var allEquip = await _equipManager.GetAllAsync();

        var results = allEquip
            .Where(e =>
                e.PlateformeNavigation.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query)) ||
                e.PlateformeNavigation.Exposers.Any(ex =>
                    ex.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query)) ||
                e.Fournirs.Any(f =>
                    f.PrestationNavigation.Type_PrestationNavigation.Nom_Type_Prestation
                        .ToLower()
                        .Contains(query))
            )
            .Select(e => new EquipementPreviewDTO
            {
                Id_Equipement = e.Id_Equipement,
                Nom_Equipement = e.Nom_Equipement,
                Description_Technique = e.Description_Technique,
                Nom_Plateforme = e.PlateformeNavigation?.Nom_Plateforme,
                Prix_Achat = e.Prix_Achat,
                Date_Acquisition = e.Date_Acquisition,
                Disponibilite = e.Disponibilite,
                MotsCles = e.PlateformeNavigation?.Specifiers
                    .Select(s => s.Mot_ClefNavigation.Nom_Mot_Clef)
                    .Distinct()
                    .ToList() ?? new List<string>(),
                Thematiques = e.PlateformeNavigation?.Exposers
                    .Select(t => t.ThematiqueNavigation.Nom_Thematique)
                    .Distinct()
                    .ToList() ?? new List<string>()
            })
            .ToList();

        return Ok(results);
    }
}
