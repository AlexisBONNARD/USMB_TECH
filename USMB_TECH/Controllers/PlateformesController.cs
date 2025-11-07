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

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateformesController : ControllerBase
    {
        private readonly IMainRepository<Plateforme, int> _manager;

        public PlateformesController(IMainRepository<Plateforme, int> manager)
        {
            _manager = manager;
        }

        // GET: api/Plateformes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Plateforme>>> GetPlateformes()
        {
            var plateformes = await _manager.GetAllAsync();
            return Ok(plateformes);
        }

        // GET: api/Plateformes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Plateforme>> GetPlateforme(int id)
        {
            var plateforme = await _manager.GetByIdAsync(id);

            if (plateforme == null)
                return NotFound($"Aucune plateforme trouvée avec l'id {id}");

            return Ok(plateforme);
        }

        // PUT: api/Plateformes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPlateforme(int id, Plateforme plateforme)
        {
            if (id != plateforme.Id_Plateforme)
                return BadRequest("L'ID de la plateforme ne correspond pas à l'objet fourni.");

            var existingPlateforme = await _manager.GetByIdAsync(id);
            if (existingPlateforme == null)
                return NotFound($"Plateforme avec l'id {id} introuvable.");

            await _manager.PutAsync(existingPlateforme, plateforme);
            return NoContent();
        }

        // POST: api/Plateformes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plateforme>> PostPlateforme(Plateforme plateforme)
        {
            if (plateforme == null)
                return BadRequest("La plateforme ne peut pas être nulle.");

            await _manager.AddAsync(plateforme);
            return CreatedAtAction(nameof(GetPlateforme), new { id = plateforme.Id_Plateforme }, plateforme);
        }

        // DELETE: api/Plateformes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlateforme(int id)
        {
            var plateforme = await _manager.GetByIdAsync(id);
            if (plateforme == null)
                return NotFound();

            await _manager.DeleteAsync(plateforme);
            return NoContent();
        }

        // IF EXISTS: api/Plateformes/5/exists
        private bool PlateformeExists(int id)
        {
            return _manager.Plateformes.Any(e => e.Id_Plateforme == id);
        }
    }
}
