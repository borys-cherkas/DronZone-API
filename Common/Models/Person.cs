using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models.Additional;
using Common.Models.Identity;

namespace Common.Models
{
    public class Person : ModelBase<string>
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonType Type { get; set; }

        public IList<Zone> Zones { get; set; }

        public IList<Drone> Drones { get; set; }

        public ApplicationUser IdentityUser { get; set; }
    }
}
