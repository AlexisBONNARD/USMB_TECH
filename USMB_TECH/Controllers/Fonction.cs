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
    public class FonctionController(IMainRepository<Fonction, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Fonction, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Fonctions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Fonction>>> GetFonctions()
        {
            var Fonctions = await _dataRepository.GetAllAsync();
            return Ok(Fonctions);
        }

        // GET: api/Fonctions/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Fonction>> GetFonction(int id)
        {
            var Fonction = await _dataRepository.GetByIdAsync(id);
            return Fonction is null ? NotFound() : Ok(Fonction);
        }

        // PUT: api/Fonctions/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutFonction(int id, Fonction Fonction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Fonction.Id_Fonction)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, Fonction);
            return NoContent();
        }

        // POST: api/Fonctions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Fonction>> PostFonction(Fonction type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataRepository.AddAsync(type);
            return CreatedAtAction(nameof(GetFonction), new { id = type.Id_Fonction }, type);
        }

        // DELETE: api/Fonctions/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFonction(int id)
        {
            var Fonction = await _dataRepository.GetByIdAsync(id);
            if (Fonction is null)
                return NotFound($"La fonction avec l'id {id} est introuvable.");

            await _dataRepository.DeleteAsync(Fonction);
            return NoContent();
        }
    }
}