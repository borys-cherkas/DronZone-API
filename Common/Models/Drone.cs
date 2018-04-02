using Common.Models.Additional;

namespace Common.Models
{
    public class Drone : ModelBase<string>
    {
        public string OwnerId { get; set; }
        public Person Owner { get; set; }

        public DroneCharacteristics Characteristics { get; set; }
    }
}
