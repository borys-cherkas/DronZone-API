using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
