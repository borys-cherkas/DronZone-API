using System.ComponentModel.DataAnnotations;
using DronZone_API.ViewModels.ZoneValidationRequest;

namespace DronZone_API.ViewModels.Zone
{
    public class AddZoneValidationRequestViewModel : ZoneValidationRequestBaseViewModel
    {
        [Required]
        public string ZoneName { get; set; }
    }
}