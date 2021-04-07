using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirportVehicles.DTOs
{
    class Passengers
    {
        public Guid FlightID { get; } = Guid.NewGuid();

        public bool IsVip { get; set; }

        [JsonPropertyName("passengers")]
        public IEnumerable<Passenger> PassengersList { get; set; }
    }
}
