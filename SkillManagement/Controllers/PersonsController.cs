using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillManagement.Data;
using SkillManagement.Models;
using SkillManagement.Dtos;
using AutoMapper;
using System.Threading.Tasks;

namespace SkillManagement.Controllers
{
    [ApiController]
    [Route("api/v1/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PersonsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/persons
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _context.Persons
                .Include(p => p.Skills)
                .ToListAsync();

            var dtos = _mapper.Map<List<PersonDto>>(persons);
            return Ok(dtos);
        }

        // GET: api/v1/persons/{id}
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            var person = await _context.Persons
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound(new { Message = $"Person with ID {id} not found." });

            var dto = _mapper.Map<PersonDto>(person);
            return Ok(dto);
        }

        // POST: api/v1/persons
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = _mapper.Map<Person>(dto);

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<PersonDto>(person);

            return CreatedAtAction(
                nameof(GetPersonById),
                new { id = person.Id },
                resultDto);
        }

        // PUT: api/v1/persons/{id}
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePerson(long id, [FromBody] UpdatePersonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPerson = await _context.Persons
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPerson == null)
                return NotFound(new { Message = $"Person with ID {id} not found." });

            // Обновляем только изменённые поля
            _mapper.Map(dto, existingPerson);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/v1/persons/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound(new { Message = $"Person with ID {id} not found." });

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(long id) =>
            _context.Persons.Any(e => e.Id == id);
    }
}
