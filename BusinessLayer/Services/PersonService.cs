using System.Threading.Tasks;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<Person> GetByIdAsync(string id)
        {
            var person = _personRepository.GetSingleByPredicate(x => x.Id == id);
            return Task.FromResult(person);
        }

        public Task<Person> CreatePersonAsync(Person person)
        {
            var addedPerson = _personRepository.Add(person);
            return Task.FromResult(addedPerson);
        }

        public async Task DeletePersonAsync(string id)
        {
            var person = await GetByIdAsync(id);
            _personRepository.Delete(person);
        }
    }
}
