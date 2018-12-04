using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public abstract class ZoneValidationRequestBaseViewModel
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string ZoneName { get; set; }

        [Required]
        public double TopLeftLatitude { get; set; }

        [Required]
        public double TopLeftLongitude { get; set; }

        [Required]
        public double BottomRightLatitude { get; set; }

        [Required]
        public double BottomRightLongitude { get; set; }
    }
}
