using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillManagement.Models;
using SkillManagement.Data;

namespace SkillManagement.Controllers;

[ApiController]
[Route("api/v1/persons")]
public class PersonsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/persons
    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _context.Persons.ToListAsync();
        return Ok(persons);
    }

    // GET: api/v1/persons/{id}
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPersonById(long id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    // POST: api/v1/persons
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
    }

    // PUT: api/v1/persons/{id}
    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdatePerson(long id, [FromBody] Person updatedPerson)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingPerson = await _context.Persons.FindAsync(id);
        if (existingPerson == null)
            return NotFound();

        existingPerson.Name = updatedPerson.Name;
        existingPerson.DisplayName = updatedPerson.DisplayName;
        existingPerson.Skills = updatedPerson.Skills;

        await _context.SaveChangesAsync();
        return Ok(existingPerson);
    }

    // DELETE: api/v1/persons/{id}
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
            return NotFound();

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
