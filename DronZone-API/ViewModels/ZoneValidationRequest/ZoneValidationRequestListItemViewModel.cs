﻿using Common.Models.Additional;
using System;

namespace DronZone_API.ViewModels.ZoneValidationRequest
{
    public class ZoneValidationRequestListItemViewModel
    {
        public string Id { get; set; }

        public string TargetZoneId { get; set; }

        public ZoneValidationStatus Status { get; set; }

        public ZoneValidationType RequestType { get; set; }

        public string ZoneName { get; set; }

        public DateTime? Updated { get; set; }
    }
}
