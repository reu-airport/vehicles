using AirportVehicles.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace AirportVehicles
{
    class Vehicle
    {
        public Guid Id { get; } = Guid.NewGuid();

        public VehicleType Type { get; set; }

        public int SiteNum { get; set; }

        private IEnumerable<VehicleAction> _actionsSequence;
        public IEnumerable<VehicleAction> ActionsSequence
        {
            get => _actionsSequence;
            set
            {
                _actionsSequence = value;
                foreach (var action in _actionsSequence)
                {
                    action.Vehicle = this;
                }
            }
        }

        // Количество оставшихся запросов для данной машинки на обслуживание борта
        private int _incompleteRequestsCount = 0;

        private AutoResetEvent _waitForRequestHandle = new AutoResetEvent(false);

        public void AcceptRequestForOperating()
        {
            Interlocked.Increment(ref _incompleteRequestsCount);
            if (_incompleteRequestsCount >= 1)
                _waitForRequestHandle.Set();
        }
            


        public void Run(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                _waitForRequestHandle.WaitOne();
                foreach (var action in ActionsSequence)
                {
                    action.Run();
                }
                Interlocked.Decrement(ref _incompleteRequestsCount);
                if (Volatile.Read(ref _incompleteRequestsCount) == 0)
                    _waitForRequestHandle.Reset();
            }
        }
    }
}
