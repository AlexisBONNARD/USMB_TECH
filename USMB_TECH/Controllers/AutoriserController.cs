using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorisersController(UsmbTechDbContext context) : ControllerBase
    {
        private readonly UsmbTechDbContext _context = context;

        // GET: api/Autorisers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autoriser>>> GetAutorisers()
        {
            return Ok(await _context.Autorisers
                .Include(a => a.EquipementNavigation)
                .Include(a => a.Type_ClientNavigation)
                .ToListAsync());
        }

        // GET: api/Autorisers/{idEquipement}/{idTypeClient}
        [HttpGet("{idEquipement}/{idTypeClient}")]
        public async Task<ActionResult<Autoriser>> GetAutoriser(int idEquipement, int idTypeClient)
        {
            var autoriser = await _context.Autorisers
                .Include(a => a.EquipementNavigation)
                .Include(a => a.Type_ClientNavigation)
                .FirstOrDefaultAsync(a =>
                    a.Id_Equipement == idEquipement &&
                    a.Id_Type_Client == idTypeClient);

            return autoriser == null ? NotFound() : Ok(autoriser);
        }

        // POST: api/Autorisers
        [HttpPost]
        public async Task<ActionResult<Autoriser>> PostAutoriser(Autoriser autoriser)
        {
            _context.Autorisers.Add(autoriser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutoriser),
                new { autoriser.Id_Equipement, autoriser.Id_Type_Client },
                autoriser);
        }

        // PUT: api/Autorisers/{idEquipement}/{idTypeClient}
        [HttpPut("{idEquipement}/{idTypeClient}")]
        public async Task<IActionResult> PutAutoriser(
            int idEquipement,
            int idTypeClient,
            Autoriser autoriser)
        {
            if (idEquipement != autoriser.Id_Equipement ||
                idTypeClient != autoriser.Id_Type_Client)
            {
                return BadRequest();
            }

            _context.Entry(autoriser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Autorisers/{idEquipement}/{idTypeClient}
        [HttpDelete("{idEquipement}/{idTypeClient}")]
        public async Task<IActionResult> DeleteAutoriser(int idEquipement, int idTypeClient)
        {
            var autoriser = await _context.Autorisers
                .FirstOrDefaultAsync(a =>
                    a.Id_Equipement == idEquipement &&
                    a.Id_Type_Client == idTypeClient);

            if (autoriser == null)
                return NotFound();

            _context.Autorisers.Remove(autoriser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
