using Common.Models.Additional;

namespace Common.Models
{
    public class MapZone : ModelBase<int>
    {   
        //Center point
        public string Altitude { get; set; }
        public string Longitude { get; set; }

        public FigureType FigureType { get; set; }

        public double Size { get; set; }
    }
}
