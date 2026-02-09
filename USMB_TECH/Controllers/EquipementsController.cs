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
    public class EquipementsController(IMainRepository<Equipement, int> dataRepository, IMapper mapper, EquipementManager equipementManager) : ControllerBase
    {
        private readonly IMainRepository<Equipement, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;
        private readonly EquipementManager _equipementManager = equipementManager;

        // GET: api/Equipements
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipements()
        {
            var equipements = await _dataRepository.GetAllAsync();
            return Ok(equipements);
        }

        // GET: api/Equipements/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Equipement>> GetEquipement(int id)
        {
            var equipement = await _dataRepository.GetByIdAsync(id);
            return equipement is null ? NotFound() : Ok(equipement);
        }

        // PUT: api/Equipements/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutEquipement(int id, UpdateEquipementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id_Equipement)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Équipement avec l'id {id} introuvable.");

            //  passage par AutoMapper
            var mappedEntity = _mapper.Map<Equipement>(dto);

            await _dataRepository.UpdateAsync(existing, mappedEntity);
            return NoContent();
        }


        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Equipement>> PostEquipement(AddEquipementDTO equipementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var equipement = _mapper.Map<Equipement>(equipementDto);
            await _dataRepository.AddAsync(equipement);
            return CreatedAtAction(nameof(GetEquipement), new { id = equipement.Id_Equipement }, equipement);
        }

        // DELETE: api/Equipements/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            var equipement = await _dataRepository.GetByIdAsync(id);
            if (equipement is null)
                return NotFound($"Laboratoire avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(equipement);
            return NoContent();
        }

        // POST: api/Equipements
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Aucun fichier reçu");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "../USMB_TECH_Blazor/wwwroot/uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { url = $"/uploads/{file.FileName}" });
        }

        [HttpPost("{id}/type-clients")]
        public async Task<IActionResult> SetTypeClients(int id, [FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("Liste vide");

            await _equipementManager.SetTypeClientsAsync(id, ids);
            return NoContent();
        }

        [HttpPost("{id}/type-utilisations")]
        public async Task<IActionResult> SetTypeUtilisations(int id, [FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("Liste vide");

            await _equipementManager.SetTypeUtilisationsAsync(id, ids);
            return NoContent();
        }
    }
}