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
using USMB_TECH.DTO;
using AutoMapper;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoireController(IMainRepository<Laboratoire, string> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Laboratoire, string> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Laboratoires
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Laboratoire>>> GetLaboratoires()
        {
            var Laboratoires = await _dataRepository.GetAllAsync();
            return Ok(Laboratoires);
        }

        // GET: api/Laboratoires/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Laboratoire>> GetLaboratoire(string id)
        {
            var Laboratoire = await _dataRepository.GetByIdAsync(id);
            return Laboratoire is null ? NotFound() : Ok(Laboratoire);
        }

        // PUT: api/Laboratoires/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLaboratoire(string id, Laboratoire Laboratoire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Laboratoire.Nom_Court)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, Laboratoire);
            return NoContent();
        }

        // POST: api/Laboratoires
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Laboratoire>> PostLaboratoire(AddLaboratoireDTO laboratoireDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laboratoire = _mapper.Map<Laboratoire>(laboratoireDto);
            await _dataRepository.AddAsync(laboratoire);

            return CreatedAtAction(nameof(GetLaboratoire), new { id = laboratoire.Nom_Court }, laboratoireDto);
        }

        // DELETE: api/Laboratoires/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLaboratoire(string id)
        {
            var Laboratoire = await _dataRepository.GetByIdAsync(id);
            if (Laboratoire is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(Laboratoire);
            return NoContent();
        }
    }
}


