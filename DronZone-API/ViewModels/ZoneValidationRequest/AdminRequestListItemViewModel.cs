using Common.Models.Additional;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class AdminRequestListItemViewModel
    {
        public string Id { get; set; }

        public string TargetZoneId { get; set; }

        public string RequesterId { get; set; }
        public string RequesterName { get; set; }

        public ZoneValidationStatus Status { get; set; }

        public ZoneValidationType RequestType { get; set; }
    }
}
