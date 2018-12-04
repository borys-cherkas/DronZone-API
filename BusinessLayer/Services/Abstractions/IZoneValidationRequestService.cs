using System.Collections.Generic;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IZoneValidationRequestService
    {
        ZoneValidationRequest GetRequestById(string requestId, string currentPersonId);

        ICollection<ZoneValidationRequest> GetUserZoneRequests(string currentPersonId);

        ZoneValidationRequest GetActiveZoneRequest(string zoneId);

        ZoneValidationRequest CreateNewZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string personId);

        ZoneValidationRequest CreateModifyZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string personId);

        void CancelZoneRequest(string modelRequestId, string currentPersonId);
    }
}
