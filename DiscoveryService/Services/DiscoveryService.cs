using Grpc.Core;
using Discovery;
using DiscoveryService.Data;

namespace DiscoveryService.Services
{
    public class DiscoveryService : Discovery.DiscoveryService.DiscoveryServiceBase
    {
        public override Task<GetServicesResponse> GetServices(GetServicesRequest request, ServerCallContext context)
        {
            var response = new GetServicesResponse();
            foreach (var service in InMemoryStorageServices.GetAll())
            {
                response.Services.Add(new ServiceInfo
                {
                    ServiceName = service.Key,
                    Channel = service.Value,
                    Status = ServiceInfo.Types.Status.Working
                });
            }
            return Task.FromResult(response);
        }

        public override Task<ServiceInfo> GetChannel(ServiceNameRequest request, ServerCallContext context)
        {
            string? channel = InMemoryStorageServices.GetChannel(request.ServiceName);
            if (channel != null)
            {
                return Task.FromResult(new ServiceInfo
                {
                    ServiceName = request.ServiceName,
                    Channel = channel,
                    Status = ServiceInfo.Types.Status.Working
                });
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Service '{request.ServiceName}' not found."));
            }
        }
    }
}
