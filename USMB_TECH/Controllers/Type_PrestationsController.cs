using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using AutoMapper;
using USMB_TECH.DTO;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Type_PrestationsController(IMainRepository<Type_Prestation, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Type_Prestation, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Type_Prestations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Type_Prestation>>> GetType_Prestations()
        {
            var type_Prestations = await _dataRepository.GetAllAsync();
            return Ok(type_Prestations);
        }

        // GET: api/Type_Prestations/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Type_Prestation>> GetType_Prestation(int id)
        {
            var type_Prestation = await _dataRepository.GetByIdAsync(id);
            return type_Prestation is null ? NotFound() : Ok(type_Prestation);
        }

        // PUT: api/Type_Prestations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutType_Prestation(int id, Type_Prestation type_Prestation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != type_Prestation.Id_Type_Prestation)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, type_Prestation);
            return NoContent();
        }

        // POST: api/Type_Prestations
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Type_Prestation>> PostType_Prestation(Type_Prestation type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataRepository.AddAsync(type);
            return CreatedAtAction(nameof(GetType_Prestation), new { id = type.Id_Type_Prestation }, type);
        }

        // DELETE: api/Type_Prestations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteType_Prestation(int id)
        {
            var type_Prestation = await _dataRepository.GetByIdAsync(id);
            if (type_Prestation is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(type_Prestation);
            return NoContent();
        }
    }
}