using Common.Models.Additional;

namespace Common.Models
{
    public class AreaFilter : ModelBase<int>
    {
        public DroneType DroneType { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double MaxDroneWeigth { get; set; }

        public double MaxDroneSpeed { get; set; }

        public int ZoneSettingsId { get; set; }
        public ZoneSettings ZoneSettings { get; set; }
    }
}
