using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.Zone
{
    public class EditZoneViewModel
    {
        [Required]
        public string ZoneId { get; set; }

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
