using System.Threading.Tasks;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPersonService
    {
        Task<Person> GetByIdAsync(string id);

        Task<Person> CreatePersonAsync(Person person);

        Task DeletePersonAsync(string id);
    }
}
