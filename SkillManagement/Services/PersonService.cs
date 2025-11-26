using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillManagement.Models;
using SkillManagement.Data;

namespace SkillManagement.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;

        public PersonService(AppDbContext context)
        {
            _context = context;
        }

        // Реализуем все методы интерфейса
        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.Include(p => p.Skills).ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(long id)
        {
            return await _context.Persons
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePersonAsync(long id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return false;
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
