using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    public class PassengersRequest : VehicleDTO
    {
        public int GateNum { get; set; }

        public Guid FlightID { get; set; }

        public bool IsVip { get; set; }
    }
}
