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
    public class MarquesController(IMainRepository<Marque, int> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Marque, int> _dataRepository = dataRepository;

        // GET: api/Marques
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
            var Marques = await _dataRepository.GetAllAsync();
            return Ok(Marques);
        }

        // GET: api/Marques/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
            var Marque = await _dataRepository.GetByIdAsync(id);
            return Marque is null ? NotFound() : Ok(Marque);
        }

        // PUT: api/Marques/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutMarque(int id, Marque Marque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Marque.Id_Marque)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Marque avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, Marque);
            return NoContent();
        }

        // POST: api/Marques
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Marque>> PostMarque(Marque Marque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(Marque);
            return CreatedAtAction(nameof(GetMarque), new { id = Marque.Id_Marque }, Marque);
        }

        // DELETE: api/Marques/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            var Marque = await _dataRepository.GetByIdAsync(id);
            if (Marque is null)
                return NotFound($"Marque avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(Marque);
            return NoContent();
        }
    }
}


