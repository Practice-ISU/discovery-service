using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace DiscoveryService.Data
{
    public class TimerRequests
    {
        private readonly Timer _timer;
        private readonly int _timeSleep = 5 * 60 * 1000;

        public TimerRequests()
        {
            _timer = new Timer(_ => Task.Run(SendRequestsToServer),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(_timeSleep));
        }

        private async Task SendRequestsToServer()
        {
            foreach (var service in InMemoryStorageServices.GetAll())
            {
                var channel = GrpcChannel.ForAddress(service.Value["chanel_ping"]);
                var client = new DiscoveryPing.DiscoveryPing.DiscoveryPingClient(channel);

                try
                {
                    var response = await client.PingAsync(new DiscoveryPing.PingRequest { Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), ServiceName = service.Key });
                    if (response.Success)
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Microservice {service.Key} is not responding\nremoving from Available list\n{ex}\n");
                    InMemoryStorageServices.Delete(service.Key);
                }
            }
        }
    }
}
