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
    public class Contact_USMBController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;

        public Contact_USMBController(UsmbTechDbContext context)
        {
            _context = context;
        }

        // GET: api/Contact_USMB
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact_USMB>>> GetContact_USMBs()
        {
            return await _context.Contact_USMBs.ToListAsync();
        }

        // GET: api/Contact_USMB/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact_USMB>> GetContact_USMB(int id)
        {
            var contact_USMB = await _context.Contact_USMBs.FindAsync(id);

            if (contact_USMB == null)
            {
                return NotFound();
            }

            return contact_USMB;
        }

        // PUT: api/Contact_USMB/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact_USMB(int id, Contact_USMB contact_USMB)
        {
            if (id != contact_USMB.Id_Contact)
            {
                return BadRequest();
            }

            _context.Entry(contact_USMB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Contact_USMBExists(id))
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

        // POST: api/Contact_USMB
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact_USMB>> PostContact_USMB(Contact_USMB contact_USMB)
        {
            _context.Contact_USMBs.Add(contact_USMB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact_USMB", new { id = contact_USMB.Id_Contact }, contact_USMB);
        }

        // DELETE: api/Contact_USMB/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact_USMB(int id)
        {
            var contact_USMB = await _context.Contact_USMBs.FindAsync(id);
            if (contact_USMB == null)
            {
                return NotFound();
            }

            _context.Contact_USMBs.Remove(contact_USMB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Contact_USMBExists(int id)
        {
            return _context.Contact_USMBs.Any(e => e.Id_Contact == id);
        }
    }
}
