using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH.Models.Repository;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Type_ClientController(
        IMainRepository<Type_Client, int> dataRepository,
        IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Type_Client, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type_Client>>> GetType_Clients()
        {
            return Ok(await _dataRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Type_Client>> GetType_Client(int id)
        {
            var typeClient = await _dataRepository.GetByIdAsync(id);
            return typeClient is null ? NotFound() : Ok(typeClient);
        }

        [HttpPost]
        public async Task<ActionResult<Type_Client>> PostType_Client(AddType_ClientDTO dto)
        {
            var entity = _mapper.Map<Type_Client>(dto);
            await _dataRepository.AddAsync(entity);

            return CreatedAtAction(nameof(GetType_Client),
                new { id = entity.Id_Type_Client }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType_Client(int id, Type_Client typeClient)
        {
            if (id != typeClient.Id_Type_Client)
                return BadRequest();

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _dataRepository.UpdateAsync(existing, typeClient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType_Client(int id)
        {
            var typeClient = await _dataRepository.GetByIdAsync(id);
            if (typeClient is null)
                return NotFound();

            await _dataRepository.DeleteAsync(typeClient);
            return NoContent();
        }
    }
}
