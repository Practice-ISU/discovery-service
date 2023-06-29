using Grpc.Core;
using Discovery;
using DiscoveryService.Data;
using log4net;

namespace DiscoveryService.Services
{
    /// <summary>
    /// Represents the Discovery Service implementation.
    /// </summary>
    public class DiscoveryService : Discovery.DiscoveryService.DiscoveryServiceBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DiscoveryService));

        /// <summary>
        /// Retrieves all services from in-memory storage.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="context">The context of the server-side invocation.</param>
        /// <returns>The response containing information about all registered services.</returns>
        public override Task<GetServicesResponse> GetServices(GetServicesRequest request, ServerCallContext context)
        {
            logger.Info("Received GetServices request");

            var response = new GetServicesResponse();
            foreach (var service in InMemoryStorageServices.GetAll())
            {
                response.Services.Add(new ServiceInfo
                {
                    ServiceName = service.Key,
                    Channel = service.Value["channel"],
                    Status = ServiceInfo.Types.Status.Working
                });
            }

            logger.Info($"Returning {response.Services.Count} services");

            return Task.FromResult(response);
        }

        /// <summary>
        /// Retrieves the channel for a specific service.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="context">The context of the server-side invocation.</param>
        /// <returns>The response containing information about the specified service.</returns>
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
                logger.Warn($"Service '{request.ServiceName}' not found");
                throw new RpcException(new Status(StatusCode.NotFound, $"Service '{request.ServiceName}' not found"));
            }
        }
    }
}
