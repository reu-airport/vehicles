using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    public class BaggageRequest : VehicleDTO
    {
        public Guid FlightID { get; set; }
    }
}
