using Common.Models;

namespace DronZone_API.ViewModels.Zone
{
    public class ZoneDetailedViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MapRectangle MapRectangle { get; set; }

        public string ValidationRequestId { get; set; }

        public int SettingsId { get; set; }
    }
}
