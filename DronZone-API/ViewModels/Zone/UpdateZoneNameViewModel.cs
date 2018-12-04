using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.Zone
{
    public class UpdateZoneNameViewModel
    {
        [Required]
        public string ZoneId { get; set; }

        [Required]
        public string ZoneName { get; set; }
    }
}
