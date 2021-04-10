using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportVehicles.ActionsSequencesFactories
{
    class BaggageLoaderActionsSequenceFactory : ActionsSequenceFactory
    {
        public BaggageLoaderActionsSequenceFactory(FlightDirection direction, int siteNum) :
            base(VehicleType.BaggageLoader, direction, siteNum)
        {

        }

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

        protected override IEnumerable<VehicleAction> CreateTyped()
        {
            if (siteNum == 1)
            {
                for (int i = 0; i < garageToSiteOneRoute.Length - 1; i++)
                {
                    yield return new Movement(garageToSiteOneRoute[i], garageToSiteOneRoute[i + 1]);
                }
                for (int i = 0; i < siteOneToGarageRoute.Length - 1; i++)
                {
                    yield return new Movement(siteOneToGarageRoute[i], siteOneToGarageRoute[i + 1]);
                }
            }
                
            else if (siteNum == 2)
            {
                for (int i = 0; i < garageToSiteTwoRoute.Length - 1; i++)
                {
                    yield return new Movement(garageToSiteTwoRoute[i], garageToSiteTwoRoute[i + 1]);
                }
                for (int i = 0; i < siteTwoToGarageRoute.Length - 1; i++)
                {
                    yield return new Movement(siteTwoToGarageRoute[i], siteTwoToGarageRoute[i + 1]);
                }
            }
        }
    }
}
