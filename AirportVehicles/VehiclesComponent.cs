using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AirportVehicles
{
    class VehiclesComponent
    {
        const string vehicleRequestsQueueName = "vehicleRequest";
        const string movementRequestsQueueName = "movementRequest";
        const string movementPermissionsQueueName = "movementPermission";
        const string movementEndsQueueName = "movementEnd";
        const string passengersRequestsQueueName = "passengersRequest";
        const string passengersQueueName = "passengers";
        const string baggageRequestsQueueName = "baggageRequest";
        const string baggageQueueName = "baggage";

        private static JsonSerializerOptions serOpts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private IModel _channel;
        private EventingBasicConsumer _vehicleRequestsConsumer;
        private EventingBasicConsumer _movementPermissionsConsumer;
        private EventingBasicConsumer _passengersConsumer;
        private EventingBasicConsumer _baggageConsumer;

        public VehiclesComponent(IModel channel)
        {
            _channel = channel;

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
        }

        private void HandleBaggage(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandlePassengers(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleMovementPermission(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleVehicleRequest(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Publish<TDto>(string queueName, TDto dto)
        {
            var json = JsonSerializer.Serialize(dto, serOpts);
            var message = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish("", queueName, body: message);
        }
    }
}
