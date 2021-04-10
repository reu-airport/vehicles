using AirportVehicles.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        private VehicleAction[] _actionsSequence;
        public IEnumerable<VehicleAction> ActionsSequence
        {
            get => _actionsSequence;
            set
            {
                _actionsSequence = value.ToArray();
                foreach (var action in _actionsSequence)
                {
                    action.Vehicle = this;
                }
            }
        }

        //// Количество оставшихся запросов для данной машинки на обслуживание борта
        //private int _pendingRequestsCount = 0;

        private AutoResetEvent _gotRequestHandle = new AutoResetEvent(false);

        private ConcurrentQueue<VehicleRequest> _requests = new ConcurrentQueue<VehicleRequest>();

        public void AcceptRequestForOperating(VehicleRequest request)
        {
            _requests.Enqueue(request);
            _gotRequestHandle.Set();
            //Interlocked.Increment(ref _pendingRequestsCount);
            //if (_pendingRequestsCount >= 1)
            //    _waitForRequestHandle.Set();
        }
            


        public void Run(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                _gotRequestHandle.WaitOne();
                _requests.TryDequeue(out VehicleRequest request);
                ActionsSequence = ActionsSequenceFactory.Create(Type, request.Direction, SiteNum);
                foreach (var action in ActionsSequence)
                {
                    action.Run();
                    Thread.Sleep(2000);
                }
                VehiclesComponent.PublishHandlingEnd(new HandlingEnd { Site = SiteNum });
                if (!_requests.TryPeek(out _))
                {
                    _gotRequestHandle.Reset();
                }
                //Interlocked.Decrement(ref _pendingRequestsCount);
                //if (Volatile.Read(ref _pendingRequestsCount) == 0)
                //    _waitForRequestHandle.Reset();
            }
        }
    }
}
