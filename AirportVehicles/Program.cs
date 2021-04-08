using RabbitMQ.Client;
using System;

namespace AirportVehicles
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = connFactory.CreateConnection();
            var channel = connection.CreateModel();

            var vehiclesComponent = new VehiclesComponent(channel);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
