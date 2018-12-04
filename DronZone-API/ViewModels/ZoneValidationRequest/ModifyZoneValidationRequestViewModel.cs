using System.ComponentModel.DataAnnotations;
using DronZone_API.ViewModels.Zone;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class ModifyZoneValidationRequestViewModel : ZoneValidationRequestBaseViewModel
    {
        [Required]
        public string ZoneId { get; set; }
    }
}
