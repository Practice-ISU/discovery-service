using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using log4net;

namespace DiscoveryService.Data
{
    /// <summary>
    /// TimerRequests class is responsible for sending ping requests to all registered microservices at a regular interval.
    /// </summary>
    public class TimerRequests
    {
        private readonly Timer _timer;

        // Time interval between consecutive ping requests in milliseconds
        private readonly int _timeSleep = 5 * 60 * 1000;

        // Logger for the class
        private static readonly ILog log = LogManager.GetLogger(typeof(TimerRequests));

        /// <summary>
        /// Constructor for TimerRequests class.
        /// </summary>
        public TimerRequests()
        {
            _timer = new Timer(_ => Task.Run(SendRequestsToServer),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(_timeSleep));
        }

        /// <summary>
        /// Sends ping requests to all the registered microservices at an interval of 5 minutes.
        /// </summary>
        private async Task SendRequestsToServer()
        {
            log.Info("Starting Ping");
            foreach (var service in InMemoryStorageServices.GetAll())
            {
                var channel = GrpcChannel.ForAddress(service.Value["chanel_ping"]);
                var client = new DiscoveryPing.DiscoveryPing.DiscoveryPingClient(channel);

                log.Info($"A request is made to the client = {service.Key} using the ping channel = {service.Value["chanel_ping"]}");

                try
                {
                    var response = await client.PingAsync(new DiscoveryPing.PingRequest { Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), ServiceName = service.Key });
                    if (response.Success)
                    {
                        log.Info($"Service '{service.Key}' is available");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Microservice {service.Key} is not responding\nremoving from Available list\n{ex}");
                    InMemoryStorageServices.Delete(service.Key);
                }
            }
        }
    }
}
