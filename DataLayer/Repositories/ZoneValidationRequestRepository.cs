using System;
using System.Collections.Generic;
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

        public ZoneValidationRequest GetById(string id)
        {
            return GetSingleByPredicate(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public ICollection<ZoneValidationRequest> FindByZoneId(string zoneId)
        {
            return GetAll(x => x.TargetZoneId.Equals(zoneId, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteAllZoneRequests(string zoneId)
        {
            var listToDelete = FindByZoneId(zoneId);

            DbContext.RemoveRange(listToDelete);
            DbContext.SaveChanges();
        }
    }
}
