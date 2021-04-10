using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirportVehicles.DTOs
{
    public class Passengers : VehicleDTO
    {
        public Guid FlightID { get; set; }

        public bool IsVip { get; set; }

        public int Count { get; set; }
    }
}
