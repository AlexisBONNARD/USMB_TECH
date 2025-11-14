using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly UsmbTechDbContext context;

    public SearchController(UsmbTechDbContext ctx)
    {
        context = ctx;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipement>>> GlobalSearch([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Query is required.");

        query = query.ToLower();

        var byMotCle = context.Equipements
            .Include(e => e.PlateformeNavigation)
                .ThenInclude(p => p.Specifiers)
                    .ThenInclude(s => s.Mot_ClefNavigation)
            .Where(e =>
                e.PlateformeNavigation.Specifiers
                    .Any(s => s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            );

        var byThematique = context.Equipements
            .Include(e => e.PlateformeNavigation)
                .ThenInclude(p => p.Exposers)
                    .ThenInclude(ex => ex.ThematiqueNavigation)
            .Where(e =>
                e.PlateformeNavigation.Exposers
                    .Any(ex => ex.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query))
            );

        var byCategorie = context.Equipements
            .Include(e => e.Fournirs)
                .ThenInclude(f => f.PrestationNavigation)
                    .ThenInclude(p => p.Type_PrestationNavigation)
            .Where(e =>
                e.Fournirs.Any(f =>
                    f.PrestationNavigation.Type_PrestationNavigation.Nom_Type_Prestation
                        .ToLower()
                        .Contains(query)
                )
            );

        var results = await byMotCle
            .Union(byThematique)
            .Union(byCategorie)
            .Distinct()
            .ToListAsync();

        return Ok(results);
    }
}
