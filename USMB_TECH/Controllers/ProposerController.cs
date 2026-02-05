using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

[Route("api/[controller]")]
[ApiController]
public class ProposersController(UsmbTechDbContext context) : ControllerBase
{
    private readonly UsmbTechDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proposer>>> GetProposers()
        => Ok(await _context.Proposers
            .Include(p => p.EquipementNavigation)
            .Include(p => p.Type_UtilisationNavigation)
            .ToListAsync());

    [HttpGet("{idEquipement}/{idTypeUtilisation}")]
    public async Task<ActionResult<Proposer>> GetProposer(int idEquipement, int idTypeUtilisation)
    {
        var proposer = await _context.Proposers
            .FirstOrDefaultAsync(p =>
                p.Id_Equipement == idEquipement &&
                p.Id_Type_Utilisation == idTypeUtilisation);

        return proposer == null ? NotFound() : Ok(proposer);
    }

    [HttpPost]
    public async Task<ActionResult<Proposer>> PostProposer(Proposer proposer)
    {
        _context.Proposers.Add(proposer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProposer),
            new { proposer.Id_Equipement, proposer.Id_Type_Utilisation },
            proposer);
    }

    [HttpPut("{idEquipement}/{idTypeUtilisation}")]
    public async Task<IActionResult> PutProposer(
        int idEquipement,
        int idTypeUtilisation,
        Proposer proposer)
    {
        if (idEquipement != proposer.Id_Equipement ||
            idTypeUtilisation != proposer.Id_Type_Utilisation)
            return BadRequest();

        _context.Entry(proposer).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{idEquipement}/{idTypeUtilisation}")]
    public async Task<IActionResult> DeleteProposer(int idEquipement, int idTypeUtilisation)
    {
        var proposer = await _context.Proposers
            .FirstOrDefaultAsync(p =>
                p.Id_Equipement == idEquipement &&
                p.Id_Type_Utilisation == idTypeUtilisation);

        if (proposer == null)
            return NotFound();

        _context.Proposers.Remove(proposer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
