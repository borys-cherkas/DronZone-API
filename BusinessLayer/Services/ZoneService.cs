using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLayer.Filters;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneValidationRequestRepository _zoneValidationRequestRepository;
        private readonly IMapRectanglesRepository _mapRectanglesRepository;

        public ZoneService(
            IZoneRepository zoneRepository,
            IZoneValidationRequestRepository zoneValidationRequestRepository, 
            IMapRectanglesRepository mapRectanglesRepository)
        {
            _zoneRepository = zoneRepository;
            _zoneValidationRequestRepository = zoneValidationRequestRepository;
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

        public ICollection<Zone> GetZonesByPersonId(string personId, ZoneListFilter filter)
        {
            var zones = _zoneRepository.GetAll(x => x.OwnerId == personId && x.IsConfirmed);

            return FilterZones(zones, filter);
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

        public void UpdateZoneName(string zoneId, string zoneName, string personId)
        {
            var zoneToUpdate = GetZoneById(zoneId);
            if (zoneToUpdate == null)
            {
                throw new InvalidDataException("There is no such zone to update.");
            }

            if (zoneToUpdate.OwnerId != personId)
            {
                throw new UnauthorizedAccessException("You haven't permissions to modify requests of this zone.");
            }

            zoneToUpdate.Name = zoneName;

            _zoneRepository.Update(zoneToUpdate);
        }

        public void DeleteWithValidationRequests(string zoneId)
        {
            var zone = GetZoneById(zoneId, q => q.Include(x => x.Settings).Include(x => x.MapRectangle));
            if (zone == null)
            {
                return;
            }

            _zoneValidationRequestRepository.DeleteAllZoneRequests(zoneId);

            _zoneRepository.Delete(zone);
        }

        private ICollection<Zone> FilterZones(ICollection<Zone> unfilteredZones, ZoneListFilter filter)
        {
            IEnumerable<Zone> filteredList = unfilteredZones;

            //TODO: Of course it's better to move this filtering inside SQL query but it's boring now :)
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.ZoneName))
                {
                    filteredList = filteredList.Where(x => x.Name.ToLower().Contains(filter.ZoneName.ToLower()));
                }

                if (filter.Confirmed != null)
                {
                    filteredList = filteredList.Where(x => x.IsConfirmed == filter.Confirmed);
                }
            }

            return filteredList.ToList();
        }
    }
}