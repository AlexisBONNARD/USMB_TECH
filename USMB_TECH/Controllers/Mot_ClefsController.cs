using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Models;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mot_ClefsController(IMainRepository<Mot_Clef, int> dataRepository) : ControllerBase
    {

        private readonly IMainRepository<Mot_Clef, int> _dataRepository = dataRepository;

        // GET: api/Mot_Clefs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Mot_Clef>>> Getmot_clefs()
        {
            var mot_clefs = await _dataRepository.GetAllAsync();
            return Ok(mot_clefs);
        }

        // GET: api/Mot_Clefs/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mot_Clef>> Getmot_clef(int id)
        {
            var mot_clef = await _dataRepository.GetByIdAsync(id);
            return mot_clef is null ? NotFound() : Ok(mot_clef);
        }

        // PUT: api/Mot_Clefs/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Putmot_clef(int id, Mot_Clef mot_clef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mot_clef.Id_Mot_Clef)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"mot_clef avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, mot_clef);
            return NoContent();
        }

        // POST: api/Mot_Clefs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Mot_Clef>> Postmot_clef(Mot_Clef mot_clef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(mot_clef);
            return CreatedAtAction(nameof(Getmot_clef), new { id = mot_clef.Id_Mot_Clef }, mot_clef);
        }

        // DELETE: api/Mot_Clefs/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletemot_clef(int id)
        {
            var mot_clef = await _dataRepository.GetByIdAsync(id);
            if (mot_clef is null)
                return NotFound($"mot_clef avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(mot_clef);
            return NoContent();
        }
    }
}
