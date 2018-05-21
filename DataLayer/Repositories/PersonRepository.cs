using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class PersonRepository : RepositoryBase<Person, string>, IPersonRepository
    {
        public PersonRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.People)
        {
        }
    }
}