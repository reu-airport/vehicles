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
            },
            new Vehicle
            {
                Type = VehicleType.Stairs,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.BaggageVan,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.BaggageLoader,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.Refueler,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.CateringTruck,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.VipShuttle,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.FollowMeVan,
                SiteNum = 1,
            },
            new Vehicle
            {
                Type = VehicleType.Bus,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.Stairs,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.BaggageVan,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.BaggageLoader,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.Refueler,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.CateringTruck,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.VipShuttle,
                SiteNum = 2,
            },
            new Vehicle
            {
                Type = VehicleType.FollowMeVan,
                SiteNum = 2
            }
        };

        public static Vehicle GetByTypeAndSite(VehicleType vehicleType, int siteNum) =>
            Vehicles.Single(veh => veh.Type == vehicleType && veh.SiteNum == siteNum);

        public static Vehicle GetById(Guid id) => 
            Vehicles.Single(veh => veh.Id == id);
    }
}
