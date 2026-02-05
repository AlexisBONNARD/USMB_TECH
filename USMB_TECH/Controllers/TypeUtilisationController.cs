using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

[Route("api/[controller]")]
[ApiController]
public class TypeUtilisationsController(UsmbTechDbContext context) : ControllerBase
{
    private readonly UsmbTechDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Type_Utilisation>>> GetTypeUtilisations()
        => Ok(await _context.Type_Utilisations.ToListAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Type_Utilisation>> GetTypeUtilisation(int id)
    {
        var type = await _context.Type_Utilisations.FindAsync(id);
        return type == null ? NotFound() : Ok(type);
    }

    [HttpPost]
    public async Task<ActionResult<Type_Utilisation>> PostTypeUtilisation(Type_Utilisation type)
    {
        _context.Type_Utilisations.Add(type);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTypeUtilisation), new { id = type.Id_Type_Utilisation }, type);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTypeUtilisation(int id, Type_Utilisation type)
    {
        if (id != type.Id_Type_Utilisation)
            return BadRequest();

        _context.Entry(type).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTypeUtilisation(int id)
    {
        var type = await _context.Type_Utilisations.FindAsync(id);
        if (type == null)
            return NotFound();

        _context.Type_Utilisations.Remove(type);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
