using AirportVehicles.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirportVehicles
{
    class VehiclesComponent
    {
        const string vehicleRequestsQueueName = "vehicleRequest";
        const string movementRequestsQueueName = "movementRequest";
        const string movementPermissionsQueueName = "movementPermission";
        const string movementEndsQueueName = "movementEnd";
        const string vehicleMovementsQueueName = "vehicleMovement";
        const string passengersRequestsQueueName = "passengersRequest";
        const string passengersQueueName = "passengers";
        const string baggageRequestsQueueName = "baggageRequest";
        const string baggageQueueName = "baggage";

        private static JsonSerializerOptions serOpts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        private ConcurrentDictionary<Guid, VehicleAction> _waitingActions = 
            new ConcurrentDictionary<Guid, VehicleAction>();

        private IModel _channel;
        private EventingBasicConsumer _vehicleRequestsConsumer;
        private EventingBasicConsumer _movementPermissionsConsumer;
        private EventingBasicConsumer _passengersConsumer;
        private EventingBasicConsumer _baggageConsumer;

        public VehiclesComponent(IModel channel)
        {

            _channel = channel;
            _channel.QueueDeclare(vehicleRequestsQueueName, false, false, false, null);
            _channel.QueueDeclare(movementRequestsQueueName, false, false, false, null);
            _channel.QueueDeclare(movementPermissionsQueueName, false, false, false, null);
            _channel.QueueDeclare(movementEndsQueueName, false, false, false, null);
            _channel.QueueDeclare(passengersRequestsQueueName, false, false, false, null);
            _channel.QueueDeclare(passengersQueueName, false, false, false, null);
            _channel.QueueDeclare(baggageRequestsQueueName, false, false, false, null);
            _channel.QueueDeclare(baggageQueueName, false, false, false, null);
            _channel.QueueDeclare(vehicleMovementsQueueName, false, false, false, null);
            
            _vehicleRequestsConsumer = new EventingBasicConsumer(_channel);
            _movementPermissionsConsumer = new EventingBasicConsumer(_channel);
            _passengersConsumer = new EventingBasicConsumer(_channel);
            _baggageConsumer = new EventingBasicConsumer(_channel);

            _vehicleRequestsConsumer.Received += HandleVehicleRequest;
            _movementPermissionsConsumer.Received += HandleMovementPermission;
            _passengersConsumer.Received += HandlePassengers;
            _baggageConsumer.Received += HandleBaggage;

            channel.BasicConsume(vehicleRequestsQueueName, true, _vehicleRequestsConsumer);
            channel.BasicConsume(movementPermissionsQueueName, true, _movementPermissionsConsumer);
            channel.BasicConsume(passengersQueueName, true, _passengersConsumer);
            channel.BasicConsume(baggageQueueName, true, _baggageConsumer);

            foreach (var vehicle in Garage.Vehicles)
            {
                foreach (var action in vehicle.ActionsSequence)
                {
                    action.VehiclesComponent = this;
                }
            }
        }

        public void AddWaitingAction(Guid vehicleId, VehicleAction action) =>
            _waitingActions.TryAdd(vehicleId, action);  

        public void PublishMovementRequest(MovementRequest request) =>
            Publish(movementRequestsQueueName, request);

        public void PublishVehicleMovement(VehicleMovement movement) =>
            Publish(vehicleMovementsQueueName, movement);

        public void PublishMovementEnd(MovementEnd mEnd) =>
            Publish(movementEndsQueueName, mEnd);

        public void PublishBaggageRequest(BaggageRequest request) =>
            Publish(baggageRequestsQueueName, request);

        public void PublishPassengersRequest(PassengersRequest request) =>
            Publish(passengersRequestsQueueName, request);

        private void HandleResponse<T>(object? sender, BasicDeliverEventArgs e) where T : VehicleDTO
        {
            var response = DeserializeMessageBody<T>(e.Body);
            _waitingActions.TryGetValue(response.VehicleId, out VehicleAction action);
            _waitingActions.TryRemove(response.VehicleId, out _);
            Task.Run(() => action.AcceptResponse(response));
        }

        private void HandleBaggage(object? sender, BasicDeliverEventArgs e) =>
            HandleResponse<Baggage>(sender, e);

        private void HandlePassengers(object? sender, BasicDeliverEventArgs e) =>
            HandleResponse<Passengers>(sender, e);

        private void HandleMovementPermission(object? sender, BasicDeliverEventArgs e) =>
            HandleResponse<MovementPermission>(sender, e);

        private void HandleVehicleRequest(object? sender, BasicDeliverEventArgs e)
        {
            var request = DeserializeMessageBody<VehicleRequest>(e.Body);
            var vehicle = Garage.GetByTypeAndSite(request.VehicleType, request.Site);
            vehicle.AcceptRequestForOperating();
        }

        private void Publish<TDto>(string queueName, TDto dto)
        {
            var json = JsonSerializer.Serialize(dto, serOpts);
            var message = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish("", queueName, body: message);
        }

        private static T DeserializeMessageBody<T>(ReadOnlyMemory<byte> body)
        {
            var jsonStr = Encoding.UTF8.GetString(body.ToArray());
            return JsonSerializer.Deserialize<T>(jsonStr, serOpts);
        }
    }
}
