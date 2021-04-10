using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles
{
    static class Garage
    {
        public static IEnumerable<Vehicle> Vehicles { get; } = new Vehicle[]
        {
            new Vehicle
            {
                Type = VehicleType.Bus,
                SiteNum = 1,
                ActionsSequence = new VehicleAction[]
                {
                    new Movement(12, 11),
                    new Movement(11, 2),
                    new Movement(2, 4)
                }
            }

            //new Vehicle
            //{
            //    Type = VehicleType.Stairs,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 2),
            //        new GraphEdge(2, 4)
            //    }),
            //    SiteNum = 1
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.BaggageLoader,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 2),
            //        new GraphEdge(2, 6)
            //    }),
            //    SiteNum = 1
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.Refueler,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 2),
            //        new GraphEdge(2, 5)
            //    }),
            //    SiteNum = 1
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.CateringTruck,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 2),
            //        new GraphEdge(2, 5)
            //    }),
            //    SiteNum = 1
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.Stairs,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 8)
            //    }),
            //    SiteNum = 2
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.BaggageLoader,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 7)
            //    }),
            //    SiteNum = 2
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.Refueler,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 9)
            //    }),
            //    SiteNum = 2
            //},
            //new Vehicle
            //{
            //    Type = VehicleType.CateringTruck,
            //    Route = new Route(new GraphEdge[]
            //    {
            //        new GraphEdge(12, 11),
            //        new GraphEdge(11, 9)
            //    }),
            //    SiteNum = 2
            //}
        };

        public static Vehicle GetByTypeAndSite(VehicleType vehicleType, int siteNum) =>
            Vehicles.Single(veh => veh.Type == vehicleType && veh.SiteNum == siteNum);

        public static Vehicle GetById(Guid id) => 
            Vehicles.Single(veh => veh.Id == id);
    }
}
