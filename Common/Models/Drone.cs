using System;
using System.Collections.Generic;
using Common.Models.Additional;

namespace Common.Models
{
    public class Drone : ModelBase<string>
    {
        public string Code { get; set; }

        public DateTime? AttachedDateTime { get; set; }

        public string OwnerId { get; set; }
        public Person Owner { get; set; }

        public DroneType Type { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double Weigth { get; set; }

        public double MaxSpeed { get; set; }

        // navigation props 
        public ICollection<DronePositionSnapshot> DronePositionSnapshots { get; set; }
    }
}
