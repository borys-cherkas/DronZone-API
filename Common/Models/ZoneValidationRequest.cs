using Common.Models.Additional;

namespace Common.Models
{
    public class ZoneValidationRequest : ModelBase<string>
    {
        public ZoneValidationStatus Status { get; set; }

        public string AdministratorId { get; set; }

        public string ZoneId { get; set; }
        public Zone Zone { get; set; }

        public ZoneValidationType RequestType { get; set; }

        public string Description { get; set; }
    }
}
