using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    public class VehicleMovement
    {
        public Guid VehicleId { get; set; }

        public VehicleType Type { get; set; }

        public int VertexFrom { get; set; }

        public int VertexTo { get; set; }
    }
}
