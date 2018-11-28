using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IZoneValidationRequestService
    {
        ZoneValidationRequest CreateNewZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string requestPersonId);

        ZoneValidationRequest CreateModifyZoneRequest(
            ZoneValidationRequest zoneValidationRequest, string requestPersonId);

        void CancelZoneRequest(string modelRequestId, string currentPersonId);
    }
}
