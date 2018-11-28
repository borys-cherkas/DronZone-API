using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.Zone
{
    public class CancelZoneValidationRequestViewModel
    {
        [Required]
        public string RequestId { get; set; }
    }
}
