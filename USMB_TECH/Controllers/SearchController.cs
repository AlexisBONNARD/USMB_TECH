using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<Equipement>>> GlobalSearch([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Query is required.");

        query = query.ToLower();

        var allEquip = await _equipManager.GetAllAsync();

        var results = allEquip.Where(e =>
            e.PlateformeNavigation.Specifiers.Any(s =>
                s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query)) ||
            e.PlateformeNavigation.Exposers.Any(ex =>
                ex.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query)) ||
            e.Fournirs.Any(f =>
                f.PrestationNavigation.Type_PrestationNavigation.Nom_Type_Prestation
                    .ToLower()
                    .Contains(query))
        ).ToList();

        return Ok(results);
    }
}
