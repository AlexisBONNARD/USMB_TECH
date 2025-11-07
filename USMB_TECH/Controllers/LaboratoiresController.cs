using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoiresController(IMainRepository<Laboratoire, string> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Laboratoire, string> _dataRepository = dataRepository;

        // GET: api/Laboratoires
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Laboratoire>>> GetAll()
        {
            var laboratoires = await _dataRepository.GetAllAsync();
            return Ok(laboratoires);
        }

        // GET: api/Laboratoires/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Laboratoire>> GetById(string id)
        {
            var laboratoire = await _dataRepository.GetByIdAsync(id);
            return laboratoire is null ? NotFound() : Ok(laboratoire);
        }

        // PUT: api/Laboratoires/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLaboratoire(string id, Laboratoire laboratoire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != laboratoire.Nom_Court)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _dataRepository.UpdateAsync(existing, laboratoire);
            return NoContent();
        }

        // POST: api/Laboratoires
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Laboratoire>> PostLaboratoire(Laboratoire laboratoire)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(laboratoire);
            return CreatedAtAction(nameof(GetById), new { id = laboratoire.Nom_Court }, laboratoire);
        }

        // DELETE: api/Laboratoires/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLaboratoire(string id)
        {
            var laboratoire = await _dataRepository.GetByIdAsync(id);
            if (laboratoire is null)
                return NotFound();

            await _dataRepository.DeleteAsync(laboratoire);
            return NoContent();
        }
    }
}


