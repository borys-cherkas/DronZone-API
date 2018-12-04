using Common.Models.Additional;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class ZoneValidationRequestDetailedViewModel
    {
        public string Id { get; set; }

        public ZoneValidationStatus Status { get; set; }

        public ZoneValidationType RequestType { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Means related zone, which will be modified by this request.
        /// Can be null if this request means zone creation.
        /// </summary>
        public string TargetZoneId { get; set; }

        public string ZoneName { get; set; }

        public double TopLeftLatitude { get; set; }
        public double TopLeftLongitude { get; set; }

        public double BottomRightLatitude { get; set; }
        public double BottomRightLongitude { get; set; }
    }
}
