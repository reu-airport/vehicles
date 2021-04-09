using System;
using System.Collections.Generic;
using System.Text;

namespace AirportVehicles
{
    class PlainActionsContext : ActionsContext
    {
        public PlainActionsContext(Vehicle vehicle, params VehicleAction[] actions):
            base(vehicle, actions)
        {
            this.actions.Last.Value.Done += (s, e) => OnCompleted(this, EventArgs.Empty);
        }

        public override void Run()
        {
            currentActionNode = actions.First;
            CurrentAction.Initiate();
        }
    }
}
