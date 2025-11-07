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
    public class LaboratoiresController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;

        public LaboratoiresController(UsmbTechDbContext context)
        {
            _context = context;
        }

        // GET: api/Laboratoires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratoire>>> GetLaboratoires()
        {
            return await _context.Laboratoires.ToListAsync();
        }

        // GET: api/Laboratoires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratoire>> GetLaboratoire(string id)
        {
            var laboratoire = await _context.Laboratoires.FindAsync(id);

            if (laboratoire == null)
            {
                return NotFound();
            }

            return laboratoire;
        }

        // PUT: api/Laboratoires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratoire(string id, Laboratoire laboratoire)
        {
            if (id != laboratoire.Nom_Court)
            {
                return BadRequest();
            }

            _context.Entry(laboratoire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaboratoireExists(id))
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

        // POST: api/Laboratoires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laboratoire>> PostLaboratoire(Laboratoire laboratoire)
        {
            _context.Laboratoires.Add(laboratoire);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LaboratoireExists(laboratoire.Nom_Court))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLaboratoire", new { id = laboratoire.Nom_Court }, laboratoire);
        }

        // DELETE: api/Laboratoires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratoire(string id)
        {
            var laboratoire = await _context.Laboratoires.FindAsync(id);
            if (laboratoire == null)
            {
                return NotFound();
            }

            _context.Laboratoires.Remove(laboratoire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LaboratoireExists(string id)
        {
            return _context.Laboratoires.Any(e => e.Nom_Court == id);
        }
    }
}
