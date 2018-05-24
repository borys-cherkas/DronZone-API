using Common.Models.Additional;

namespace DronZone_API.ViewModels.Filter
{
    public class FilterDetailedViewModel
    {
        public int Id { get; set; }

        public DroneType DroneType { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double MaxDroneWeigth { get; set; }

        public double MaxDroneSpeed { get; set; }

        public int ZoneSettingsId { get; set; }

        public string AreaId { get; set; }
    }
}
