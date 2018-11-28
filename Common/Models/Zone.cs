using System.Collections.Generic;

namespace Common.Models
{
    public class Zone : ModelBase<string>
    {
        public string Name { get; set; }

        public MapRectangle MapRectangle { get; set; }

        public ZoneValidationRequest ValidationRequest { get; set; }

        public string OwnerId { get; set; }

        public bool IsConfirmed { get; set; }

        public ZoneSettings Settings { get; set; }
    }
}