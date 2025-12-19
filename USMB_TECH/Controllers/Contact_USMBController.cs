using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.DTO;
using USMB_TECH.Mapper;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Contact_USMBController(IMainRepository<Contact_USMB, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Contact_USMB, int> _dataRepository = dataRepository;

        private readonly IMapper _mapper = mapper;

        // GET: api/Contact_USMBs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Contact_USMB>>> GetContact_USMBs()
        {
            var contact_USMB = await _dataRepository.GetAllAsync();
            return Ok(contact_USMB);
        }

        // GET: api/Contact_USMBs/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact_USMB>> GetContact_USMB(int id)
        {
            var contact_USMB = await _dataRepository.GetByIdAsync(id);
            return contact_USMB is null ? NotFound() : Ok(contact_USMB);
        }

        // PUT: api/Contact_USMBs/{id}
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
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, contact_USMB);
            return NoContent();
        }

        // POST: api/Contact_USMBs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Contact_USMB>> PostContact_USMB(AddContactDTO contact_USMB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = _mapper.Map<Contact_USMB>(contact_USMB);
            await _dataRepository.AddAsync(contact);
            return CreatedAtAction(nameof(GetContact_USMB), new { id = contact.Id_Contact }, contact);
        }

        // DELETE: api/Contact_USMBs/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContact_USMB(int id)
        {
            var contact_USMB = await _dataRepository.GetByIdAsync(id);
            if (contact_USMB is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(contact_USMB);
            return NoContent();
        }
    }
}