using System.ComponentModel.DataAnnotations;
using DronZone_API.ViewModels.Zone;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class AssignZoneValidationRequestViewModel
    {
        [Required]
        public string RequestId { get; set; }
    }
}
