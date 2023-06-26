using Grpc.Core;
using Discovery;
using DiscoveryService.Data;
using log4net;

namespace DiscoveryService.Services
{
    public class DiscoveryService : Discovery.DiscoveryService.DiscoveryServiceBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DiscoveryService));

        public override Task<GetServicesResponse> GetServices(GetServicesRequest request, ServerCallContext context)
        {
            logger.Info("Received GetServices request");

            var response = new GetServicesResponse();
            foreach (var service in InMemoryStorageServices.GetAll())
            {
                response.Services.Add(new ServiceInfo
                {
                    ServiceName = service.Key,
                    Channel = service.Value["chanel"],
                    Status = ServiceInfo.Types.Status.Working
                });
            }

            logger.Info($"Returning {response.Services.Count} services");

            return Task.FromResult(response);
        }

        public override Task<ServiceInfo> GetChannel(ServiceNameRequest request, ServerCallContext context)
        {
            logger.Info($"Received GetChannel request for service '{request.ServiceName}'");

            string? channel = InMemoryStorageServices.GetChannel(request.ServiceName);

            if (channel != null)
            {
                logger.Info($"Found channel for service '{request.ServiceName}'.");
                return Task.FromResult(new ServiceInfo
                {
                    ServiceName = request.ServiceName,
                    Channel = channel,
                    Status = ServiceInfo.Types.Status.Working
                });
            }
            else
            {
                logger.Error($"Error pinging service {service.Key} with channel {service.Value["chanel_ping"]}\n{ex}");
                throw new RpcException(new Status(StatusCode.NotFound, $"Service '{request.ServiceName}' not found"));
            }
        }
    }
}
