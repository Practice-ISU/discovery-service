using DiscoveryRegistration;
using DiscoveryService.Data;
using Grpc.Core;
using log4net;

namespace DiscoveryService.Services
{
    public class ServiceRegistration : DiscoveryRegistration.ServiceRegistration.ServiceRegistrationBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceRegistration));
        public override Task<ServiceResponse> Registration(ServiceRequest request, ServerCallContext context)
        {
            logger.Info($"Received registration request for service '{request.ServiceName}', channel '{request.Channel}'");
            if (request.ServiceName == "" || request.Channel == "" || request.ChannelPing == "")
            {
                logger.Error($"Error registering service '{request.ServiceName}' with channel '{request.Channel}' and ping chanel '{request.ChannelPing}'");
                return Task.FromResult(new ServiceResponse
                {
                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Success = false
                });
            }

            InMemoryStorageServices.Set(request.ServiceName, request.Channel, request.ChannelPing);

            logger.Info($"Service {request.ServiceName} registered successfully");

            return Task.FromResult(new ServiceResponse
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Success = true
            });
        }

    }
}