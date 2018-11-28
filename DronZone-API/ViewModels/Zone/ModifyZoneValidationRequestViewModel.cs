using System.ComponentModel.DataAnnotations;

namespace DronZone_API.ViewModels.Zone
{
    public class ModifyZoneValidationRequestViewModel : ZoneValidationRequestBaseViewModel
    {
        [Required]
        public string ZoneId { get; set; }
    }
}
