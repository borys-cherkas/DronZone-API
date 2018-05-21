namespace Common.Models
{
    public class ZoneSettings : ModelBase<int>
    {
        public string ZoneId { get; set; }
        public Zone Zone { get; set; }


        // filters
        // loggings
    }
}
