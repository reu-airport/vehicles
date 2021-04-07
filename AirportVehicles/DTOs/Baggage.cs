using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    class Baggage
    {
        public Guid FlightID { get; set; }

        public int BaggageCount { get; set; }
    }
}
