using Common.Models.Additional;

namespace Common.Models
{
    public class DroneCharacteristics : ModelBase<int>
    {
        public DroneType Type { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double Weigth { get; set; }

        public double MaxSpeed { get; set; }
    }
}
