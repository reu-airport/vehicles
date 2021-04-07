using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    class MovementPermission
    {
        public Guid VehicleId { get; set; }

        public int VertexFrom { get; set; }

        public int VertexTo { get; set; }

        public bool IsPermitted { get; set; }
    }
}
