using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class ZoneValidationRequestRepository : RepositoryBase<ZoneValidationRequest, string>, IZoneValidationRequestRepository
    {
        public ZoneValidationRequestRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.ZoneValidationRequests)
        {
        }
    }
}
