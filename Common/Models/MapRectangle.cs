namespace Common.Models
{
    public class MapRectangle : ModelBase<int>
    {   
        public double West { get; set; }
        public double East { get; set; }
        public double South { get; set; }
        public double North { get; set; }

        public string ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
