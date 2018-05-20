using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IZoneService
    {
        Zone GetZoneById(string zoneId, Func<IQueryable<Zone>, IQueryable<Zone>> predicate = null);

        ICollection<Zone> GetAllZones();

        ICollection<Zone> GetAllUnconfirmedZones();

        ICollection<Zone> GetZonesByPersonId(string personId);

        Zone Add(Zone zone);

        void Delete(string zoneId);
    }
}
