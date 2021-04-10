using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using AirportVehicles.ActionsSequencesFactories;
using System.Linq;

namespace AirportVehicles
{
    abstract class ActionsSequenceFactory
    {
        protected VehicleType type;
        protected FlightDirection direction;
        protected int siteNum;

        protected ActionsSequenceFactory(VehicleType type, FlightDirection direction, int siteNum)
        {
            this.type = type;
            this.direction = direction;
            this.siteNum = siteNum;
        }

        protected abstract IEnumerable<VehicleAction> CreateTyped();

        public static IEnumerable<VehicleAction> Create(VehicleType type, FlightDirection direction, int siteNum)
        {
            switch (type)
            {
                case VehicleType.FollowMeVan:
                    return new FollowMeActionsSequenceFactory(direction, siteNum).CreateTyped();
                case VehicleType.Stairs:
                    return new StairsActionsSequenceFactory(direction, siteNum).CreateTyped(); ;
                case VehicleType.Bus:
                    return new BusActionsSequenceFactory(direction, siteNum).CreateTyped();
                case VehicleType.BaggageLoader:
                    return new BaggageLoaderActionsSequenceFactory(direction, siteNum).CreateTyped();
                case VehicleType.BaggageVan:
                    return new BaggageVanActionsSequenceFactory(direction, siteNum).CreateTyped();
                case VehicleType.VipShuttle:
                    return new VipActionsSequenceFactory(direction, siteNum).CreateTyped();
                case VehicleType.Refueler:
                case VehicleType.CateringTruck:
                    return new GenericActionsSequenceFactory(type, direction, siteNum).CreateTyped();
                default:
                    return Enumerable.Empty<VehicleAction>();
            }
        }
        //public static IEnumerable<VehicleAction> 
    }
}
