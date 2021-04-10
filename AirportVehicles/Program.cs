using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

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

            VehiclesComponent.Initialize(channel, new System.Net.Http.HttpClient());

            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            foreach (var vehicle in Garage.Vehicles)
            {
                Task.Run(() => vehicle.Run(ct));
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

            cts.Cancel();
            channel.Dispose();
        }
    }
}
