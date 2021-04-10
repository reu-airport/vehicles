using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles.ActionsSequencesFactories
{
    class BusActionsSequenceFactory : ActionsSequenceFactory
    {
        public BusActionsSequenceFactory(FlightDirection direction, int siteNum): 
            base(VehicleType.Bus, direction, siteNum)
        {

        }

        private static int[] garageToDepartureGateRoute = new int[]
        {
            12, 13, 16
        };
        private static int[] departureGateToSiteOneRoute = new int[]
        {
            16, 24, 23, 22, 2, 4
        };
        private static int[] departureGateToSiteTwoRoute = new int[]
        {
            16, 24, 23, 22, 2, 11, 8
        };
        private static int[] siteOneToGarageRoute = new int[]
        {
            4, 2, 11, 12
        };
        private static int[] siteTwoToGarageRoute = new int[]
        {
            8, 11, 12
        };
        private static int[] garageToSiteOneRoute =>
            siteOneToGarageRoute.Reverse().ToArray();

        private static int[] garageToSiteTwoRoute =>
            siteTwoToGarageRoute.Reverse().ToArray();

        private static int[] siteOneToArrivalGateRoute = new int[]
        {
            4, 2, 22, 21, 20, 19, 18
        };
        private static int[] siteTwoToArrivalGateRoute = new int[]
        {
            8, 11, 2, 22, 21, 20, 19, 18
        };
        private static int[] arrivalGateToGarageRoute = new int[]
        {
            18, 13, 12
        };


        protected override IEnumerable<VehicleAction> CreateTyped()
        {
            if (direction == FlightDirection.Departure)
            {
                
                for (int i = 0; i < garageToDepartureGateRoute.Length - 1; i++)
                {
                    yield return new Movement(garageToDepartureGateRoute[i], garageToDepartureGateRoute[i + 1]);
                }
                if (siteNum == 1)
                {
                    for (int i = 0; i < departureGateToSiteOneRoute.Length - 1; i++)
                    {
                        yield return new Movement(departureGateToSiteOneRoute[i], departureGateToSiteOneRoute[i + 1]);
                    }
                    for (int i = 0; i < siteOneToGarageRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToGarageRoute[i], siteOneToGarageRoute[i + 1]);
                    }
                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < departureGateToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(departureGateToSiteTwoRoute[i], departureGateToSiteTwoRoute[i + 1]);
                    }
                    for (int i = 0; i < siteTwoToGarageRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteTwoToGarageRoute[i], siteTwoToGarageRoute[i + 1]);
                    }
                }
            }
            if (direction == FlightDirection.Arrival)
            {
                if (siteNum == 1)
                {
                    for (int i = 0; i < garageToSiteOneRoute.Length - 1; i++)
                    {
                        yield return new Movement(garageToSiteOneRoute[i], garageToSiteOneRoute[i + 1]);
                    }
                    for (int i = 0; i < siteOneToArrivalGateRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToArrivalGateRoute[i], siteOneToArrivalGateRoute[i + 1]);
                    }
                    
                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < garageToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(garageToSiteTwoRoute[i], garageToSiteTwoRoute[i + 1]);
                    }
                    for (int i = 0; i < siteTwoToArrivalGateRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteTwoToArrivalGateRoute[i], siteTwoToArrivalGateRoute[i + 1]);
                    }
                }
                for (int i = 0; i < arrivalGateToGarageRoute.Length - 1; i++)
                {
                    yield return new Movement(arrivalGateToGarageRoute[i], arrivalGateToGarageRoute[i + 1]);
                }
            }
        }
    }
}
