using DiscoveryRegistration;
using DiscoveryService.Data;
using Grpc.Core;

namespace DiscoveryService.Services
{
    public class ServiceRegistration : DiscoveryRegistration.ServiceRegistration.ServiceRegistrationBase
    {
        public override Task<ServiceResponse> Registration(ServiceRequest request, ServerCallContext context)
        {
            if (request.ServiceName=="" || request.Channel == "")
            {
                return Task.FromResult(new ServiceResponse
                {
                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Success = false
                });
            }

            InMemoryStorageServices.Set(request.ServiceName, request.Channel);

            return Task.FromResult(new ServiceResponse
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Success = true
            });
        }
    }
}
