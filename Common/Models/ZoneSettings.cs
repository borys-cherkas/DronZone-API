using System.Collections.Generic;

namespace Common.Models
{
    public class ZoneSettings : ModelBase<int>
    {
        public string ZoneId { get; set; }
        public Zone Zone { get; set; }
        
        public ICollection<AreaFilter> AreaFilters { get; set; }
    }
}
