using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportVehicles
{
    abstract class VehicleAction
    {
        public VehiclesComponent VehiclesComponent { get; set; }
        public Vehicle Vehicle { get; set; }

        protected abstract void Do(object arg);

        protected abstract void MakeRequest();

        public void Initiate()
        {
            VehiclesComponent?.AddWaitingAction(Vehicle?.Id ?? Guid.Empty, this);
            MakeRequest();
        }

        public void AcceptResponse<T>(T response)
        {
            Do(response);
            Done?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Done;
    }
}
