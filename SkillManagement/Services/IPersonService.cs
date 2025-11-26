using System.Threading.Tasks;
using SkillManagement.Models;

namespace SkillManagement.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(long id);
        Task<Person> CreatePersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(long id);
    }
}
