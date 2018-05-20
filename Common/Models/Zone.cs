using Common.Models.Additional;

namespace Common.Models
{
    public class Zone : ModelBase<string>
    {
        public string Name { get; set; }

        public MapRectangle MapRectangle { get; set; }

        public string OwnerId { get; set; }

        public bool IsConfirmed { get; set; }

        public int SettingsId { get; set; }
        public ZoneSettings Settings { get; set; }
    }
}
