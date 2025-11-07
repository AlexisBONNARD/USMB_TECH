using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateformesController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;

        public PlateformesController(UsmbTechDbContext context)
        {
            _context = context;
        }

        // GET: api/Plateformes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plateforme>>> GetPlateformes()
        {
            return await _context.Plateformes.ToListAsync();
        }

        // GET: api/Plateformes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plateforme>> GetPlateforme(int id)
        {
            var plateforme = await _context.Plateformes.FindAsync(id);

            if (plateforme == null)
            {
                return NotFound();
            }

            return plateforme;
        }

        // PUT: api/Plateformes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlateforme(int id, Plateforme plateforme)
        {
            if (id != plateforme.Id_Plateforme)
            {
                return BadRequest();
            }

            _context.Entry(plateforme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlateformeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plateformes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plateforme>> PostPlateforme(Plateforme plateforme)
        {
            _context.Plateformes.Add(plateforme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlateforme", new { id = plateforme.Id_Plateforme }, plateforme);
        }

        // DELETE: api/Plateformes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlateforme(int id)
        {
            var plateforme = await _context.Plateformes.FindAsync(id);
            if (plateforme == null)
            {
                return NotFound();
            }

            _context.Plateformes.Remove(plateforme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlateformeExists(int id)
        {
            return _context.Plateformes.Any(e => e.Id_Plateforme == id);
        }
    }
}
