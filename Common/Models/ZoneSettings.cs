namespace Common.Models
{
    public class ZoneSettings : ModelBase<int>
    {
        public string OwnerId { get; set; }
        public Person Owner { get; set; }

        // filters
        // loggings
    }
}
