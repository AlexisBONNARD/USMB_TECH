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
    public class LaboratoiresController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;
        private readonly IMainRepository<Laboratoire,string> _dataRepository;

        public LaboratoiresController(IMainRepository<Laboratoire,string> dataRepository, UsmbTechDbContext context)
        {
            _dataRepository = dataRepository;
            _context = context;
        }

        // GET: api/Laboratoires
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Laboratoire>>> GetLaboratoires()
        {
            IEnumerable<Laboratoire> laboratoires = await _dataRepository.GetAllAsync();
            return new ActionResult<IEnumerable<Laboratoire>>(laboratoires);
        }

        // GET: api/Laboratoires/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Laboratoire>> GetLaboratoire(string id)
        {
            var laboratoire = await _dataRepository.GetByIdAsync(id);

            if (laboratoire == null)
                return NotFound($"Aucune laboratoire trouvée avec l'id {id}");

            return laboratoire;
        }

        // PUT: api/Laboratoires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLaboratoire(string id, Laboratoire laboratoire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != laboratoire.Nom_Court)
            {
                return BadRequest("L'id Doit être identique à celui envoyé");
            }

            var laboratoireToUpdate = await _dataRepository.GetByIdAsync(id);

            if (laboratoireToUpdate == null)
                return NotFound($"Plateforme avec l'id {id} introuvable.");
            
            await _dataRepository.UpdateAsync(laboratoireToUpdate, laboratoire);
            return NoContent();
        }

        // POST: api/Laboratoires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Laboratoire>> PostLaboratoire(Laboratoire laboratoire)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dataRepository.AddAsync(laboratoire);
            return CreatedAtAction("GetLaboratoire", new { id = laboratoire.Nom_Court }, laboratoire);
        }

        // DELETE: api/Laboratoires/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLaboratoire(string id)
        {
            ActionResult<Laboratoire?> laboratoire = await _dataRepository.GetByIdAsync(id);
            if (laboratoire.Value == null)
            {
                return NotFound();
            }

            await _dataRepository.DeleteAsync(laboratoire.Value);

            return NoContent();
        }

        private bool LaboratoireExists(string id)
        {
            return _context.Laboratoires.Any(e => e.Nom_Court == id);
        }
    }
}
              

    


