using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    public class Baggage : VehicleDTO
    {
        public Guid FlightID { get; set; }

        public int BaggageCount { get; set; }
    }
}
