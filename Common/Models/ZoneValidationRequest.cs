using Common.Models.Additional;

namespace Common.Models
{
    public class ZoneValidationRequest : ModelBase<string>
    {
        public ZoneValidationStatus Status { get; set; }

        public string RequesterId { get; set; }

        public Person Requester { get; set; }
        public string ResponsiblePersonId { get; set; }
        public Person ResponsiblePerson { get; set; }

        public ZoneValidationType RequestType { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Means related zone, which will be modified by this request.
        /// Can be null if this request means zone creation (and hasn't been finished yet).
        /// </summary>
        public string TargetZoneId { get; set; }

        public Zone TargetZone { get; set; }

        /// <summary>.
        /// Can be null if this request means zone modifying.
        /// </summary>
        public string ZoneName { get; set; }

        public double TopLeftLatitude { get; set; }
        public double TopLeftLongitude { get; set; }

        public double BottomRightLatitude { get; set; }
        public double BottomRightLongitude { get; set; }
    }
}
