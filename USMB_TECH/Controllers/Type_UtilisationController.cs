using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Type_UtilisationController(
        IMainRepository<Type_Utilisation, int> dataRepository,
        IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Type_Utilisation, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type_Utilisation>>> GetType_Utilisations()
        {
            return Ok(await _dataRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Type_Utilisation>> GetType_Utilisation(int id)
        {
            var typeUtilisation = await _dataRepository.GetByIdAsync(id);
            return typeUtilisation is null ? NotFound() : Ok(typeUtilisation);
        }

        [HttpPost]
        public async Task<ActionResult<Type_Utilisation>> PostType_Utilisation(AddType_UtilisationDTO dto)
        {
            var entity = _mapper.Map<Type_Utilisation>(dto);
            await _dataRepository.AddAsync(entity);

            return CreatedAtAction(nameof(GetType_Utilisation),
                new { id = entity.Id_Type_Utilisation }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType_Utilisation(int id, Type_Utilisation typeUtilisation)
        {
            if (id != typeUtilisation.Id_Type_Utilisation)
                return BadRequest();

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _dataRepository.UpdateAsync(existing, typeUtilisation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType_Utilisation(int id)
        {
            var typeUtilisation = await _dataRepository.GetByIdAsync(id);
            if (typeUtilisation is null)
                return NotFound();

            await _dataRepository.DeleteAsync(typeUtilisation);
            return NoContent();
        }
    }
}
