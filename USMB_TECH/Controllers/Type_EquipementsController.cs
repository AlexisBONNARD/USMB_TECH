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
    public class Type_EquipementsController(IMainRepository<Type_Equipement, int> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Type_Equipement, int> _dataRepository = dataRepository;

        // GET: api/Type_Equipements
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Type_Equipement>>> GetType_Equipements()
        {
            var Type_Equipements = await _dataRepository.GetAllAsync();
            return Ok(Type_Equipements);
        }

        // GET: api/Type_Equipements/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Type_Equipement>> GetType_Equipement(int id)
        {
            var Type_Equipement = await _dataRepository.GetByIdAsync(id);
            return Type_Equipement is null ? NotFound() : Ok(Type_Equipement);
        }

        // PUT: api/Type_Equipements/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutType_Equipement(int id, Type_Equipement Type_Equipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Type_Equipement.Id_Type_Equipement)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, Type_Equipement);
            return NoContent();
        }

        // POST: api/Type_Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Type_Equipement>> PostType_Equipement(Type_Equipement type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataRepository.AddAsync(type);
            return CreatedAtAction(nameof(GetType_Equipement), new { id = type.Id_Type_Equipement }, type);
        }

        // DELETE: api/Type_Equipements/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteType_Equipement(int id)
        {
            var Type_Equipement = await _dataRepository.GetByIdAsync(id);
            if (Type_Equipement is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(Type_Equipement);
            return NoContent();
        }
    }
}