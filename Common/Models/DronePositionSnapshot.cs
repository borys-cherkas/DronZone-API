using System;
using Common.Models.Additional;

namespace Common.Models
{
    public class DronePositionSnapshot : ModelBase<string>
    {
        public string DroneId { get; set; }

        public MapPoint Position { get; set; }

        // if position is not so accurate
        public double Radius { get; set; }

        public DateTime When { get; set; }
    }
}
