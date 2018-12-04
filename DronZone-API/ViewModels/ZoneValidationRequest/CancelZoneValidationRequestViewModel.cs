using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class CancelZoneValidationRequestViewModel
    {
        [Required]
        public string RequestId { get; set; }
    }
}
