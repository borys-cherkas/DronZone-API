using System.ComponentModel.DataAnnotations;
using System.Net.Cache;
using Microsoft.AspNetCore.Identity;

namespace Common.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }
    }
}
