using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles
{
    class Route
    {
        public IEnumerable<GraphEdge> EdgesSequence { get; }

        public Route Reversed => new Route(
            EdgesSequence.Select(edge => edge.Reversed).Reverse()
            );

        public Route(IEnumerable<GraphEdge> edgesSequence)
        {
            EdgesSequence = edgesSequence;
        }

        public Route ContinuedWith(Route other) => 
            new Route(EdgesSequence.Concat(other.EdgesSequence));
    }
}
