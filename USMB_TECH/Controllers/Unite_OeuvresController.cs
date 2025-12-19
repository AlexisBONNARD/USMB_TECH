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
    public class Unite_OeuvresController(IMainRepository<Unite_Oeuvre, int> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Unite_Oeuvre, int> _dataRepository = dataRepository;

        // GET: api/Unite_Oeuvres
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Unite_Oeuvre>>> GetUnite_Oeuvres()
        {
            var unite_Oeuvres = await _dataRepository.GetAllAsync();
            return Ok(unite_Oeuvres);
        }

        // GET: api/Unite_Oeuvres/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unite_Oeuvre>> GetUnite_Oeuvre(int id)
        {
            var unite_Oeuvre = await _dataRepository.GetByIdAsync(id);
            return unite_Oeuvre is null ? NotFound() : Ok(unite_Oeuvre);
        }

        // PUT: api/Unite_Oeuvres/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUnite_Oeuvre(int id, Unite_Oeuvre unite_Oeuvre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unite_Oeuvre.Id_Unite_Oeuvre)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Unite_Oeuvre avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, unite_Oeuvre);
            return NoContent();
        }

        // POST: api/Unite_Oeuvres
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Unite_Oeuvre>> PostUnite_Oeuvre(Unite_Oeuvre unite_Oeuvre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(unite_Oeuvre);
            return CreatedAtAction(nameof(GetUnite_Oeuvre), new { id = unite_Oeuvre.Id_Unite_Oeuvre }, unite_Oeuvre);
        }

        // DELETE: api/Unite_Oeuvres/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUnite_Oeuvre(int id)
        {
            var unite_Oeuvre = await _dataRepository.GetByIdAsync(id);
            if (unite_Oeuvre is null)
                return NotFound($"Unite_Oeuvre avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(unite_Oeuvre);
            return NoContent();
        }
    }
}


