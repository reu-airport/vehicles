using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirportVehicles.DTOs
{
    public class Time
    {
        [JsonPropertyName("time")]
        public int UnixTimeMs { get; set; }

        public long Factor { get; set; }
    }
}
