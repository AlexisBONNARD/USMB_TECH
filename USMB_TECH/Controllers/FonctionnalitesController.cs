using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Models;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FonctionnalitesController(IMainRepository<Fonctionnalite, int> dataRepository) : ControllerBase
    {
        private readonly IMainRepository<Fonctionnalite, int> _dataRepository = dataRepository;

        // GET: api/Fonctionnalites
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Fonctionnalite>>> Getfonctionnalites()
        {
            var fonctionnalites = await _dataRepository.GetAllAsync();
            return Ok(fonctionnalites);
        }

        // GET: api/Fonctionnalites/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Fonctionnalite>> Getfonctionnalite(int id)
        {
            var fonctionnalite = await _dataRepository.GetByIdAsync(id);
            return fonctionnalite is null ? NotFound() : Ok(fonctionnalite);
        }

        // PUT: api/Fonctionnalites/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Putfonctionnalite(int id, Fonctionnalite fonctionnalite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fonctionnalite.Id_Fonctionnalite)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"fonctionnalite avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, fonctionnalite);
            return NoContent();
        }

        // POST: api/Fonctionnalites
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Fonctionnalite>> Postfonctionnalite(Fonctionnalite fonctionnalite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(fonctionnalite);
            return CreatedAtAction(nameof(Getfonctionnalite), new { id = fonctionnalite.Id_Fonctionnalite }, fonctionnalite);
        }

        // DELETE: api/Fonctionnalites/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletefonctionnalite(int id)
        {
            var fonctionnalite = await _dataRepository.GetByIdAsync(id);
            if (fonctionnalite is null)
                return NotFound($"fonctionnalite avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(fonctionnalite);
            return NoContent();
        }
    }
}
