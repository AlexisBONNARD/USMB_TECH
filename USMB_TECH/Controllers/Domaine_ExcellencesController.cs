using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Domaine_ExcellencesController(IMainRepository<Domaine_Excellence, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Domaine_Excellence, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Domaine_Excellences
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Domaine_Excellence>>> GetDomaine_Excellences()
        {
            var Domaine_Excellences = await _dataRepository.GetAllAsync();
            return Ok(Domaine_Excellences);
        }

        // GET: api/Domaine_Excellences/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Domaine_Excellence>> GetDomaine_Excellence(int id)
        {
            var Domaine_Excellence = await _dataRepository.GetByIdAsync(id);
            return Domaine_Excellence is null ? NotFound() : Ok(Domaine_Excellence);
        }

        // PUT: api/Domaine_Excellences/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutDomaine_Excellence(int id, Domaine_Excellence Domaine_Excellence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Domaine_Excellence.Id_Domaine_Excellence)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Domaine Excellence avec l'id {id} introuvable.");

            // Mise à jour du domaine d'excellence
            await _dataRepository.UpdateAsync(existing, Domaine_Excellence);

            return NoContent();
        }

        // POST: api/Domaine_Excellences
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Type_Equipement>> PostDomaine_Excellence(Domaine_Excellence Domaine_Excellence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataRepository.AddAsync(Domaine_Excellence);
            return CreatedAtAction(nameof(GetDomaine_Excellences), new { id = Domaine_Excellence.Id_Domaine_Excellence }, Domaine_Excellence);
        }

        // DELETE: api/Domaine_Excellences/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDomaine_Excellence(int id)
        {
            var Domaine_Excellence = await _dataRepository.GetByIdAsync(id);
            if (Domaine_Excellence is null)
                return NotFound($"Domaine Excellence avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(Domaine_Excellence);
            return NoContent();
        }

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
    }
}
