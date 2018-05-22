using System.ComponentModel.DataAnnotations;
using Common.Models.Additional;

namespace DronZone_API.ViewModels
{
    public class AddAreaFilterViewModel
    {
        [Required]
        public string AreaId { get; set; }

        public DroneType DroneType { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double MaxDroneWeigth { get; set; }

        public double MaxDroneSpeed { get; set; }
    }
}
