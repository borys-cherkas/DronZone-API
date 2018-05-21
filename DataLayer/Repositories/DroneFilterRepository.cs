using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class DroneFilterRepository  : RepositoryBase<DroneFilter, int>, IDroneFilterRepository
    {
        public DroneFilterRepository(AppDbContext dbContext) 
            : base(dbContext, dbContext.DroneFilters) { }
    }
}
