using System.Collections.Generic;
using Common.Models;

namespace DataLayer.Repositories.Abstractions
{
    public interface IZoneValidationRequestRepository : IRepositoryBase<ZoneValidationRequest>
    {
        ZoneValidationRequest GetById(string id);

        ICollection<ZoneValidationRequest> FindByZoneId(string zoneId);

        void DeleteAllZoneRequests(string zoneId);
    }
}
