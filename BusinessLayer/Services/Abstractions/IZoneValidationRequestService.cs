using System.Collections.Generic;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IZoneValidationRequestService
    {
        ZoneValidationRequest GetRequestById(string requestId, string currentPersonId);

        ICollection<ZoneValidationRequest> GetUserZoneRequests(string currentPersonId);

        ICollection<ZoneValidationRequest> GetUntakenZoneRequests();

        ICollection<ZoneValidationRequest> GetTakenByUserActiveZoneRequests(string userId);

        ZoneValidationRequest GetActiveZoneRequest(string zoneId);

        ZoneValidationRequest CreateNewZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string personId);

        ZoneValidationRequest CreateModifyZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string personId);

        void AssignToUser(string requestId, string currentPersonId);

        void ConfirmZoneRequest(string requestId, string currentPersonId);
        void DeclineZoneRequest(string requestId, string currentPersonId);

        void CancelZoneRequest(string requestId, string currentPersonId);
    }
}
