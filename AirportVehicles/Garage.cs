using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    using VehiclesByTypeDict = Dictionary<VehicleType, Vehicle>;

    static class Garage
    {
        static IDictionary<int, VehiclesByTypeDict> VehiclesBySites =
            new Dictionary<int, VehiclesByTypeDict>
            {
                [1] = new VehiclesByTypeDict
                {
                    [VehicleType.Stairs] = new Vehicle
                    {
                        Type = VehicleType.Stairs,
                        Route = new Route(new GraphEdge[]
                        {
                            new GraphEdge(12, 11),
                            new GraphEdge(11, 2),
                            new GraphEdge(2, 4)
                        })
                    },
                    [VehicleType.BaggageLoader] = new Vehicle
                    {
                        Type = VehicleType.BaggageLoader,
                        Route = new Route(new GraphEdge[]
                        {
                            new GraphEdge(12, 11),
                            new GraphEdge(11, 2),
                            new GraphEdge(2, 6),
                        })
                    }
                }
            };
    }
}
