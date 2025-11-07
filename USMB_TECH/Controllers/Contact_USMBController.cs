using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Contact_USMBController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;
        private readonly IMainRepository<Contact_USMB, int> _dataRepository;

        public Contact_USMBController(IMainRepository<Contact_USMB, int> dataRepository, UsmbTechDbContext context)
        {
            _dataRepository = dataRepository;
            _context = context;
        }

        // GET: api/Contact_USMB
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Contact_USMB>>> GetContact_USMBs()
        {
            IEnumerable<Contact_USMB> laboratoires = await _dataRepository.GetAllAsync();
            return new ActionResult<IEnumerable<Contact_USMB>>(laboratoires);
        }

        // GET: api/Contact_USMB/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact_USMB>> GetContact_USMB(int id)
        {
            var contact_USMB = await _dataRepository.GetByIdAsync(id);

            if (contact_USMB == null)
            {
                return NotFound();
            }

            return contact_USMB;
        }

        // PUT: api/Contact_USMB/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutContact_USMB(int id, Contact_USMB contact_USMB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != contact_USMB.Id_Contact)
            {
                return BadRequest("L'id Doit être identique à celui envoyé");
            }

            var contact_USMBToUpdate = await _dataRepository.GetByIdAsync(id);

            if (contact_USMBToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _dataRepository.UpdateAsync(contact_USMBToUpdate, contact_USMB);
                return NoContent();
            }
        }

        // POST: api/Contact_USMB
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Contact_USMB>> PostContact_USMB(Contact_USMB contact_USMB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataRepository.AddAsync(contact_USMB);

            return CreatedAtAction("GetContact_USMB", new { id = contact_USMB.Nom_Court }, contact_USMB);
        }

        // DELETE: api/Contact_USMB/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContact_USMB(int id)
        {
            ActionResult<Contact_USMB?> contact_USMB = await _dataRepository.GetByIdAsync(id);
            if (contact_USMB.Value == null)
            {
                return NotFound();
            }

            await _dataRepository.DeleteAsync(contact_USMB.Value);

            return NoContent();
        }

        private bool Contact_USMBExists(int id)
        {
            return _context.Contact_USMBs.Any(e => e.Id_Contact == id);
        }
    }
}
