using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.People)
        {
        }
    }
}