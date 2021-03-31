using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    class GraphEdge
    {
        public int VertexFrom { get; }

        public int VertexTo { get; }

        public Vehicle? CurrentVehicle { get; private set; }

        public GraphEdge Reversed { get; }

        public GraphEdge(int vertexFrom, int vertexTo)
        {
            VertexFrom = vertexFrom;
            VertexTo = vertexTo;
            Reversed = new GraphEdge(vertexTo, vertexFrom);
        }
        
        public void Occupy(Vehicle vehicle)
        {
            CurrentVehicle = vehicle;
            Reversed.CurrentVehicle = vehicle;
        }

        public void Free()
        {
            CurrentVehicle = null;
            Reversed.CurrentVehicle = null;
        }
    }
}
