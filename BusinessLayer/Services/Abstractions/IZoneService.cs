using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Filters;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IZoneService
    {
        Zone GetZoneById(string zoneId, Func<IQueryable<Zone>, IQueryable<Zone>> predicate = null);

        ICollection<Zone> GetAllZones();

        ICollection<Zone> GetAllUnconfirmedZones();

        ICollection<Zone> GetZonesByPersonId(string personId, ZoneListFilter filter);

        bool ValidateName(string zoneId, string zoneName, string currentPersonId);

        Zone Add(Zone zone);

        void Update(Zone zone);

        void UpdateZoneName(string modelZoneId, string modelZoneName, string currentPersonId);

        void DeleteWithValidationRequests(string zoneId);
    }
}
