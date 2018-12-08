using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using Common.Models.Additional;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ZoneValidationRequestService : IZoneValidationRequestService
    {
        private readonly IZoneValidationRequestRepository _zoneValidationRequestRepository;
        private readonly IZoneService _zoneService;

        public ZoneValidationRequestService(
            IZoneValidationRequestRepository zoneValidationRequestRepository,
            IZoneService zoneService)
        {
            _zoneValidationRequestRepository = zoneValidationRequestRepository;
            _zoneService = zoneService;
        }

        public ZoneValidationRequest GetRequestById(string requestId, string currentPersonId)
        {
            var request = _zoneValidationRequestRepository.GetSingleByPredicate(r => r.Id == requestId);
            if (request == null)
            {
                throw new ArgumentException("Cannot find such validation request.");
            }

            if (request.RequesterId != currentPersonId && currentPersonId != "admin")
            {
                throw new ArgumentException("You haven't access to see this request.");
            }

            return request;
        }

        public ICollection<ZoneValidationRequest> GetUserZoneRequests(string currentPersonId)
        {
            return _zoneValidationRequestRepository.GetAll(r => r.RequesterId == currentPersonId);
        }

        public ICollection<ZoneValidationRequest> GetUntakenZoneRequests()
        {
            return _zoneValidationRequestRepository.GetAll(r =>
                string.IsNullOrWhiteSpace(r.ResponsiblePersonId) && r.Status == ZoneValidationStatus.WaitingForAdministrator);
        }

        public ICollection<ZoneValidationRequest> GetTakenByUserActiveZoneRequests(string userId)
        {
            return _zoneValidationRequestRepository.GetAll(r =>
                r.ResponsiblePersonId == userId && r.Status == ZoneValidationStatus.InProgress);
        }

        public ZoneValidationRequest GetActiveZoneRequest(string zoneId)
        {
            return _zoneValidationRequestRepository.GetSingleByPredicate(r => r.TargetZoneId != null 
                                                                && r.TargetZoneId.Equals(zoneId, StringComparison.OrdinalIgnoreCase)
                                                                && (r.Status == ZoneValidationStatus.InProgress || r.Status == ZoneValidationStatus.WaitingForAdministrator));
        }


        public ZoneValidationRequest CreateNewZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string requestPersonId)
        {
            zoneValidationRequest.RequesterId = requestPersonId;

            return _zoneValidationRequestRepository.Add(zoneValidationRequest);
        }


        public ZoneValidationRequest CreateModifyZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string requestPersonId)
        {
            var dbZone = _zoneService.GetZoneById(zoneValidationRequest.TargetZoneId);
            if (dbZone == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dbZone.Id), "Invalid ZoneId.");
            }

            if (dbZone.OwnerId != requestPersonId)
            {
                throw new ArgumentOutOfRangeException(nameof(dbZone.Id), "You haven't permissions to modify this zone.");
            }

            var zoneRequests = _zoneValidationRequestRepository.FindByZoneId(dbZone.Id);
            if (zoneRequests.Any(x => x.Status < ZoneValidationStatus.Declined))
            {
                throw new ArgumentOutOfRangeException(nameof(dbZone.Id),
                    "Cannot add modifying request while has another active one for this zone.");
            }

            zoneValidationRequest.RequesterId = requestPersonId;
            
            return _zoneValidationRequestRepository.Add(zoneValidationRequest);
        }

        public void AssignToUser(string requestId, string responsiblePersonId)
        {
            var dbRequest = _zoneValidationRequestRepository.GetById(requestId);
            if (dbRequest == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dbRequest.Id), "Invalid ZoneValidationRequestId.");
            }

            if (dbRequest.Status >= ZoneValidationStatus.Declined)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot assign closed request.");
            }

            dbRequest.ResponsiblePersonId = responsiblePersonId;
            dbRequest.Status = ZoneValidationStatus.InProgress;

            _zoneValidationRequestRepository.Update(dbRequest);
        }

        public void ConfirmZoneRequest(string requestId, string currentPersonId)
        {
            var dbRequest = _zoneValidationRequestRepository.GetById(requestId);
            if (dbRequest == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dbRequest.Id), "Invalid ZoneValidationRequestId.");
            }

            if (dbRequest.Status >= ZoneValidationStatus.Declined)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot confirm closed request.");
            }

            if (dbRequest.ResponsiblePersonId != currentPersonId)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot confirm someone else's request.");
            }

            var zone = new Zone
            {
                Id = dbRequest.TargetZoneId,
                MapRectangle = new MapRectangle(),
                Settings = new ZoneSettings(),
                OwnerId = dbRequest.RequesterId,
                IsConfirmed = true,
                Name = dbRequest.ZoneName
            };
            zone.MapRectangle.TopLeftLatitude = dbRequest.TopLeftLatitude;
            zone.MapRectangle.TopLeftLongitude = dbRequest.TopLeftLongitude;
            zone.MapRectangle.BottomRightLatitude = dbRequest.BottomRightLatitude;
            zone.MapRectangle.BottomRightLongitude = dbRequest.BottomRightLongitude;

            if (dbRequest.RequestType == ZoneValidationType.Creation)
            {
                _zoneService.Add(zone);
            }
            else
            {
                var oldZone = _zoneService.GetZoneById(zone.Id, q => q.Include(x => x.MapRectangle));

                zone.Name = oldZone.Name;
                zone.Settings = oldZone.Settings;
                zone.MapRectangle.Id = oldZone.MapRectangle.Id;

                _zoneService.Update(zone);
            }

            dbRequest.Status = ZoneValidationStatus.Confirmed;
            _zoneValidationRequestRepository.Update(dbRequest);
        }

        public void DeclineZoneRequest(string requestId, string currentPersonId)
        {
            var dbRequest = _zoneValidationRequestRepository.GetById(requestId);
            if (dbRequest == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dbRequest.Id), "Invalid ZoneValidationRequestId.");
            }

            if (dbRequest.Status >= ZoneValidationStatus.Declined)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot decline closed request.");
            }

            if (dbRequest.ResponsiblePersonId != currentPersonId)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot decline someone else's request.");
            }

            dbRequest.Status = ZoneValidationStatus.Declined;
            _zoneValidationRequestRepository.Update(dbRequest);
        }

        public void CancelZoneRequest(string requestId, string requestPersonId)
        {
            var dbRequest = _zoneValidationRequestRepository.GetById(requestId);
            if (dbRequest == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dbRequest.Id), "Invalid ZoneValidationRequestId.");
            }

            if (dbRequest.RequesterId != requestPersonId)
            {
                throw new ArgumentOutOfRangeException(nameof(dbRequest.RequesterId), "You haven't permissions to modify requests of this zone.");
            }

            if (dbRequest.Status >= ZoneValidationStatus.Declined)
            {
                throw new ArgumentOutOfRangeException(nameof(requestId), "You cannot cancel closed request.");
            }

            dbRequest.Status = ZoneValidationStatus.Canceled;

            _zoneValidationRequestRepository.Update(dbRequest);
        }
    }
}