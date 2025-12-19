using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;
using AutoMapper;
using USMB_TECH.DTO;

namespace USMB_TECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prise_ContactsController(IMainRepository<Prise_Contact, int> dataRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMainRepository<Prise_Contact, int> _dataRepository = dataRepository;
        private readonly IMapper _mapper = mapper;

        // GET: api/Prise_Contacts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Prise_Contact>>> GetPrise_Contacts()
        {
            var prise_Contacts = await _dataRepository.GetAllAsync();
            return Ok(prise_Contacts);
        }

        // GET: api/Prise_Contacts/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Prise_Contact>> GetPrise_Contact(int id)
        {
            var prise_Contacts = await _dataRepository.GetByIdAsync(id);
            return prise_Contacts is null ? NotFound() : Ok(prise_Contacts);
        }

        // PUT: api/Prise_Contacts/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPrise_Contact(int id, Prise_Contact prise_Contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prise_Contacts.Num_Prise_Contact)
                return BadRequest("L'identifiant de la ressource ne correspond pas à celui du corps de la requête.");

            var existing = await _dataRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound($"Prise_Contact avec l'id {id} introuvable.");

            await _dataRepository.UpdateAsync(existing, prise_Contacts);
            return NoContent();
        }

        // POST: api/Prise_Contacts
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Prise_Contact>> PostPrise_Contact(Prise_Contact prise_Contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dataRepository.AddAsync(prise_Contacts);
            return CreatedAtAction(nameof(GetPrise_Contact), new { id = prise_Contacts.Num_Prise_Contact }, prise_Contacts);
        }

        // DELETE: api/Prise_Contacts/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePrise_Contact(int id)
        {
            var Prise_Contact = await _dataRepository.GetByIdAsync(id);
            if (Prise_Contact is null)
                return NotFound($"Prise_Contact avec l'id {id} introuvable.");

            await _dataRepository.DeleteAsync(Prise_Contact);
            return NoContent();
        }

        // POST: api/Prise_Contacts
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
