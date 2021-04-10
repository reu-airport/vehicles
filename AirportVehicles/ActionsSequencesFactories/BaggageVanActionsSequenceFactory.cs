using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles.ActionsSequencesFactories
{
    class BaggageVanActionsSequenceFactory : ActionsSequenceFactory
    {
        public BaggageVanActionsSequenceFactory(FlightDirection direction, int siteNum) :
            base(VehicleType.BaggageVan, direction, siteNum)
        {

        }

        private static int[] garageToBaggageGateRoute = new int[]
        {
            12, 13, 14
        };
        private static int[] baggageGateToSiteOneRoute = new int[]
        {
            14, 13, 24, 23, 22, 2, 6
        };
        private static int[] baggageGateToSiteTwoRoute = new int[]
        {
            14, 13, 24, 23, 22, 2, 11, 7
        };
        private static int[] siteOneToGarageRoute = new int[]
        {
            6, 2, 11, 12
        };
        private static int[] siteTwoToGarageRoute = new int[]
        {
            7, 11, 12
        };
        private static int[] garageToSiteOneRoute =>
            siteOneToGarageRoute.Reverse().ToArray();

        private static int[] garageToSiteTwoRoute =>
            siteTwoToGarageRoute.Reverse().ToArray();

        private static int[] siteOneToBaggageGateRoute = new int[]
        {
            6, 2, 22, 23, 24, 13, 14
        };
        private static int[] siteTwoToBaggageGateRoute = new int[]
        {
            7, 11, 2, 22, 23, 24, 13, 14
        };
        private static int[] baggageGateToGarageRoute =>
            garageToBaggageGateRoute.Reverse().ToArray();

        protected override IEnumerable<VehicleAction> CreateTyped()
        {
            if (direction == FlightDirection.Departure)
            {

                for (int i = 0; i < garageToBaggageGateRoute.Length - 1; i++)
                {
                    yield return new Movement(garageToBaggageGateRoute[i], garageToBaggageGateRoute[i + 1]);
                }
                if (siteNum == 1)
                {
                    for (int i = 0; i < baggageGateToSiteOneRoute.Length - 1; i++)
                    {
                        yield return new Movement(baggageGateToSiteOneRoute[i], baggageGateToSiteOneRoute[i + 1]);
                    }
                    for (int i = 0; i < siteOneToGarageRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToGarageRoute[i], siteOneToGarageRoute[i + 1]);
                    }
                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < baggageGateToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(baggageGateToSiteTwoRoute[i], baggageGateToSiteTwoRoute[i + 1]);
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
                    for (int i = 0; i < siteOneToBaggageGateRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToBaggageGateRoute[i], siteOneToBaggageGateRoute[i + 1]);
                    }

                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < garageToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(garageToSiteTwoRoute[i], garageToSiteTwoRoute[i + 1]);
                    }
                    for (int i = 0; i < siteTwoToBaggageGateRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteTwoToBaggageGateRoute[i], siteTwoToBaggageGateRoute[i + 1]);
                    }
                }
                for (int i = 0; i < baggageGateToGarageRoute.Length - 1; i++)
                {
                    yield return new Movement(baggageGateToGarageRoute[i], baggageGateToGarageRoute[i + 1]);
                }
            }
        }
    }
}
