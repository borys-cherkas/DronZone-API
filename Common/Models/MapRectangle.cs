using System.Drawing;

namespace Common.Models
{
    public class MapRectangle : ModelBase<int>
    {    
        public double TopLeftLatitude { get; set; }
        public double TopLeftLongitude { get; set; }

        public double BottomRightLatitude { get; set; }
        public double BottomRightLongitude { get; set; }

        public string ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
