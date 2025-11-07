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

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementsController : ControllerBase
    {
        private readonly UsmbTechDbContext _context;
        private readonly IMainRepository<Equipement, int> _dataRepository;

        public EquipementsController(UsmbTechDbContext context, IMainRepository<Equipement, int> dataRepository)
        {
            _context = context;
            _dataRepository = dataRepository;
        }

        // GET: api/Equipements
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipements()
        {
            var equipements = await _dataRepository.GetAllAsync();
            if (equipements == null || !equipements.Any())
                return NotFound();
            return Ok(equipements);
        }

        // GET: api/Equipements/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Equipement>> GetEquipement(int id)
        {
            var resultat = await _dataRepository.GetByIdAsync(id);
            return resultat == null ? NotFound() : Ok(resultat);
        }

        // PUT: api/Equipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutEquipement(int id, Equipement equipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Récupérer la marque existante
            Equipement? equipementAModifier = await _dataRepository.GetByIdAsync(id);

            if (equipementAModifier == null)
            {
                return NotFound();
            }

            equipement.Id_Equipement = id; // Conserver l'ID

            await _dataRepository.UpdateAsync(equipementAModifier, equipement);

            return NoContent();
        }

        // POST: api/Equipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Equipement>> PostEquipement(Equipement equipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Sauvegarde de la  marque
            await _dataRepository.AddAsync(equipement);

            // Retourner le détail de la marque  créé
            return CreatedAtAction("Get", new { id = equipement.Id_Equipement }, equipement);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            ActionResult<Equipement?> brand = await _dataRepository.GetByIdAsync(id);
            if (brand.Value == null)
                return NotFound();
            await _dataRepository.DeleteAsync(brand.Value);
            return NoContent();
        }

        private bool EquipementExists(int id)
        {
            return _context.Equipements.Any(e => e.Id_Equipement == id);
        }
    }
}
