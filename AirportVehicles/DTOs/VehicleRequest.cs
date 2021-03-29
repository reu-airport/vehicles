using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{

    public class VehicleRequest
    {
        public VehicleType VehicleType { get; set; }

        public int Site { get; set; }
    }
}
