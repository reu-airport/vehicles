using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    class GraphEdge
    {
        public int VertexFrom { get; }

        public int VertexTo { get; }

        public Vehicle? CurrentVehicle { get; set; }

        public GraphEdge(int vertexFrom, int vertexTo)
        {
            VertexFrom = vertexFrom;
            VertexTo = vertexTo;
        }
    }
}
