using System.Collections.Generic;
using Common.Models.Additional;

namespace Common.Models
{
    public class Person : ModelBase<string>
    {
        public PersonType Type { get; set; }

        public IList<Zone> Zones { get; set; }

        public IList<Drone> Drones { get; set; }
    }
}
