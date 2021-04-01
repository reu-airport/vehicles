﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirportVehicles
{
    class Vehicle
    {
        [JsonPropertyName("vehicleId")]
        public Guid Id { get; } = Guid.NewGuid();

        [JsonPropertyName("vehicleType")]
        public VehicleType Type { get; set; }

        public Route Route { get; set; }
    }
}
