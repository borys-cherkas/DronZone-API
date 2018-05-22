using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class AreaFilterRepository  : RepositoryBase<AreaFilter, int>, IAreaFilterRepository
    {
        public AreaFilterRepository(AppDbContext dbContext) 
            : base(dbContext, dbContext.AreaFilters) { }
    }
}
