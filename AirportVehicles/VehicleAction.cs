using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportVehicles
{
    abstract class VehicleAction
    {
        public Vehicle Vehicle { get; set; }

        protected abstract void Resume(object arg);

        protected abstract void MakeRequest();

        public void Initiate()
        {
            VehiclesComponent.AddWaitingAction(Vehicle?.Id ?? Guid.Empty, this);
            MakeRequest();
        }

        private object locker = new object();
        protected object response;

        public virtual void AcceptResponse(object response)
        {
            lock(locker)
                this.response = response;
            waitForResponseHandle.Set();
        }

        protected AutoResetEvent waitForResponseHandle { get; } = new AutoResetEvent(false);

        public void Run()
        {
            Initiate();
            waitForResponseHandle.WaitOne();
            Resume(response);
        }
    }
}
