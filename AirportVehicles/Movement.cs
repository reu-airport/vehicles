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
            VehiclesComponent?.PublishMovementRequest(
                new MovementRequest
                {
                    VehicleId = Vehicle?.Id ?? Guid.Empty,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo
                });
        }

        protected override void Do(object arg)
        {
            var response = (MovementPermission)arg;
            while(!response.IsPermitted)
            {
                Thread.Sleep(_movementRequestPollIntervalMs);
                MakeRequest();
            }
            VehiclesComponent?.PublishVehicleMovement(
                new VehicleMovement
                {
                    VehicleId = Vehicle.Id,
                    Type = Vehicle.Type,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo
                });
            Thread.Sleep(_movementTimeMs);
            VehiclesComponent?.PublishMovementEnd(
                new MovementEnd 
                { 
                    VehicleId = Vehicle.Id,
                    VertexFrom = _vertexFrom,
                    VertexTo = _vertexTo,
                });
        }
    }
}
