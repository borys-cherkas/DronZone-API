using System;

namespace Common.Models
{
    public class DronePositionSnapshot : ModelBase<string>
    {
        public string DroneId { get; set; }

        // Position point
        public string Altitude { get; set; }
        public string Longitude { get; set; }

        // if position is not so accurate
        public double Radius { get; set; }

        public DateTime When { get; set; }
    }
}
