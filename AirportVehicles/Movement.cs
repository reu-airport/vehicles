using AirportVehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportVehicles
{
    class Movement : VehicleAction
    {
        private int _vertexFrom;
        private int _vertexTo;

        private const int _movementTimeMs = 1000;
        private const int _movementRequestPollIntervalMs = _movementTimeMs / 2;

        public Movement(int vertexFrom, int vertexTo)
        {
            _vertexFrom = vertexFrom;
            _vertexTo = vertexTo;
        }

        protected override void MakeRequest()
        {
            VehiclesComponent.PublishMovementRequest(
                new MovementRequest
                {
                    VehicleId = Vehicle?.Id ?? Guid.Empty,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo
                });
        }

        private object locker = new object();
        public override void AcceptResponse(object response)
        {
            MovementPermission currentPermission;
            lock(locker)
            {
                currentPermission = (MovementPermission)this.response;
                if (currentPermission == null || !currentPermission.IsPermitted)
                {
                    this.response = response;
                }
            }
            waitForResponseHandle.Set();
            
        }

        protected override void Resume(object arg)
        {
            var response = (MovementPermission)this.response;
            while(!response.IsPermitted)
            {
                waitForResponseHandle.Reset();
                Thread.Sleep(_movementRequestPollIntervalMs);
                Initiate();
                waitForResponseHandle.WaitOne();
                response = (MovementPermission)this.response;
            }
            
            VehiclesComponent.PublishVehicleMovement(
                new VehicleMovement
                {
                    VehicleId = Vehicle.Id,
                    VehicleType = Vehicle.Type,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo,
                    DurationMs = _movementTimeMs

                });
            Thread.Sleep(_movementTimeMs);
            VehiclesComponent.PublishMovementEnd(
                new MovementEnd 
                { 
                    VehicleId = Vehicle.Id,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo,
                });
            
        }
    }
}
