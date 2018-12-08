using System.Collections.Generic;

namespace Common.Models
{
    public class Zone : ModelBase<string>
    {
        public string Name { get; set; }

        public MapRectangle MapRectangle { get; set; }

        public string OwnerId { get; set; }

        public ZoneSettings Settings { get; set; }
    }
}