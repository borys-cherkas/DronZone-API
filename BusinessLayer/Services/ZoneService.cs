using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IMapRectanglesRepository _mapRectanglesRepository;

        public ZoneService(IZoneRepository zoneRepository, IMapRectanglesRepository mapRectanglesRepository)
        {
            _zoneRepository = zoneRepository;
            _mapRectanglesRepository = mapRectanglesRepository;
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

        public void Update(Zone zone)
        {
            var zoneToUpdate = GetZoneById(zone.Id);
            if (zoneToUpdate == null)
            {
                throw new InvalidDataException("There is no such zone to update.");
            }

            var mapRectangleToUpdate = _mapRectanglesRepository.GetByZoneId(zoneToUpdate.Id);
            if (mapRectangleToUpdate == null)
            {
                throw new InvalidDataException("Can't find zone's map rectangle.");
            }

            zoneToUpdate.Name = zone.Name;

            mapRectangleToUpdate.TopLeftLatitude = zone.MapRectangle.TopLeftLatitude;
            mapRectangleToUpdate.TopLeftLongitude = zone.MapRectangle.TopLeftLongitude;
            mapRectangleToUpdate.BottomRightLatitude = zone.MapRectangle.BottomRightLatitude;
            mapRectangleToUpdate.BottomRightLongitude = zone.MapRectangle.BottomRightLongitude;

            _zoneRepository.Update(zoneToUpdate);
            _mapRectanglesRepository.Update(mapRectangleToUpdate);
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
