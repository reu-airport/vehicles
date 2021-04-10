using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles.ActionsSequencesFactories
{
    class FollowMeActionsSequenceFactory : ActionsSequenceFactory
    {
        public FollowMeActionsSequenceFactory(FlightDirection direction, int siteNum) :
            base(VehicleType.FollowMeVan, direction, siteNum)
        {

        }
        private static int[] garageToArrivalAirstripRoute = new int[]
        {
            12, 11, 2, 25
        };
        private static int[] arrivalAirstripToSiteOneRoute = new int[]
        {
            25, 2, 3
        };
        private static int[] arrivalAirstripToSiteTwoRoute = new int[]
        {
            25, 2, 11, 10
        };
        private static int[] siteOneToDepartureAirstripRoute = new int[]
        {
            3, 2, 1
        };
        private static int[] siteTwoToDepartureAirstripRoute = new int[]
        {
            10, 11, 2, 1
        };
        private static int[] departureAirstripToGarage = new int[]
        {
            1, 2, 11, 12
        };
        private static int[] garageToSiteOneRoute = new int[]
        {
            12, 11, 2, 5
        };

        private static int[] garageToSiteTwoRoute = new int[]
        {
            12, 11, 10
        };
        private static int[] siteOneToGarageRoute = new int[]
        {
            5,2,11,12
        };

        private static int[] siteTwoToGarageRoute = new int[]
        {
            10, 11, 12
        };

        protected override IEnumerable<VehicleAction> CreateTyped()
        {
            if (direction == FlightDirection.Departure)
            {
                if (siteNum == 1)
                {
                    for (int i = 0; i < garageToSiteOneRoute.Length - 1; i++)
                    {
                        yield return new Movement(garageToSiteOneRoute[i], garageToSiteOneRoute[i + 1]);
                    }
                    for (int i = 0; i < siteOneToDepartureAirstripRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToDepartureAirstripRoute[i], siteOneToDepartureAirstripRoute[i + 1]);
                    }
                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < garageToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(garageToSiteTwoRoute[i], garageToSiteTwoRoute[i + 1]);
                    }
                    for (int i = 0; i < siteTwoToDepartureAirstripRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteTwoToDepartureAirstripRoute[i], siteTwoToDepartureAirstripRoute[i + 1]);
                    }
                }
                for (int i = 0; i < departureAirstripToGarage.Length - 1; i++)
                {
                    yield return new Movement(departureAirstripToGarage[i], departureAirstripToGarage[i + 1]);
                }
            }
            if (direction == FlightDirection.Arrival)
            {
                for (int i = 0; i < garageToArrivalAirstripRoute.Length - 1; i++)
                {
                    yield return new Movement(garageToArrivalAirstripRoute[i], garageToArrivalAirstripRoute[i + 1]);
                }
                if (siteNum == 1)
                {
                    for (int i = 0; i < arrivalAirstripToSiteOneRoute.Length - 1; i++)
                    {
                        yield return new Movement(arrivalAirstripToSiteOneRoute[i], arrivalAirstripToSiteOneRoute[i + 1]);
                    }
                    for (int i = 0; i < siteOneToGarageRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteOneToGarageRoute[i], siteOneToGarageRoute[i + 1]);
                    }
                }
                else if (siteNum == 2)
                {
                    for (int i = 0; i < arrivalAirstripToSiteTwoRoute.Length - 1; i++)
                    {
                        yield return new Movement(arrivalAirstripToSiteTwoRoute[i], arrivalAirstripToSiteTwoRoute[i + 1]);
                    }
                    for (int i = 0; i < siteTwoToGarageRoute.Length - 1; i++)
                    {
                        yield return new Movement(siteTwoToGarageRoute[i], siteTwoToGarageRoute[i + 1]);
                    }
                }
                for (int i = 0; i < arrivalAirstripToSiteOneRoute.Length - 1; i++)
                {
                    yield return new Movement(arrivalAirstripToSiteOneRoute[i], arrivalAirstripToSiteOneRoute[i + 1]);
                }
            }
        }
    }
}
