using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepository _zoneRepository;

        public ZoneService(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public Zone GetZoneById(string zoneId, Func<IQueryable<Zone>, IQueryable<Zone>> predicate = null)
        {
            return _zoneRepository.GetSingleByPredicate(x => x.Id == zoneId, predicate);
        }

        public ICollection<Zone> GetAllZones()
        {
            return _zoneRepository.GetAll();
        }

        public ICollection<Zone> GetAllUnconfirmedZones()
        {
            return _zoneRepository.GetAll(x => !x.IsConfirmed);
        }

        public ICollection<Zone> GetZonesByPersonId(string personId)
        {
            return _zoneRepository.GetAll(x => x.OwnerId == personId);
        }

        public Zone Add(Zone zone)
        {
            if (zone.Settings == null)
            {
                zone.Settings = new ZoneSettings();
            }

            var newZone = _zoneRepository.Add(zone);
            return newZone;
        }

        public void Delete(string zoneId)
        {
            var zone = GetZoneById(zoneId, q => q.Include(x => x.Settings).Include(x => x.MapRectangle));
            if (zone == null)
            {
                return;
            }

            _zoneRepository.Delete(zone);
        }
    }
}
