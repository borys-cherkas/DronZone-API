using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DronZone_API.ViewModels.Zone
{
    public class AddZoneViewModel
    {
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
