using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    class PassengersRequest
    {
        public int GateNum { get; set; }

        public Guid FlightID { get; set; }

        public bool IsVip { get; set; }

        public int Count { get; set; }
    }
}
