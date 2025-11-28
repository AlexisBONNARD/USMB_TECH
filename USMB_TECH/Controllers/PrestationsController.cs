using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestationsController(IMainRepository<Prestation, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Prestation, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Prestations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Prestation>>> GetPrestations()
        {
            var prestations = await _dataRepository.GetAllAsync();
            return Ok(prestations);
        }

        // GET: api/Prestations/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Prestation>> GetPrestation(int id)
        {
            var prestations = await _dataRepository.GetByIdAsync(id);
            return prestations is null ? NotFound() : Ok(prestations);
        }

        // PUT: api/Prestations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPrestation(int id, Prestation prestations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prestations.Id_Prestation)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, prestations);
            return NoContent();
        }

        // POST: api/Prestations
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Prestation>> PostPrestation(Prestation prestations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(prestations);
            return CreatedAtAction(nameof(GetPrestation), new { id = prestations.Id_Prestation }, prestations);
        }

        // DELETE: api/Prestations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePrestation(int id)
        {
            var prestations = await _dataRepository.GetByIdAsync(id);
            if (prestations is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(prestations);
            return NoContent();
        }
    }
}
