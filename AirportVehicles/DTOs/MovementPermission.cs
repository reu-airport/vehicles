using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles.DTOs
{
    public class MovementPermission : VehicleDTO
    {
        public int VertexFrom { get; set; }

        public int VertexTo { get; set; }

        public bool IsPermitted { get; set; }
    }
}
