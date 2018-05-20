using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.Zone
{
    public class AddZoneViewModel
    {
        [Required]
        public string ZoneName { get; set; }

        [Required]
        public double West { get; set; }

        [Required]
        public double East { get; set; }

        [Required]
        public double South { get; set; }

        [Required]
        public double North { get; set; }
    }
}
