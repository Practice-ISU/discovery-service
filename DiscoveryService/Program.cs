using DiscoveryService.Data;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Properties/log4net.config", Watch = true)]

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscoveryService.Services.DiscoveryService>();
app.MapGrpcService<DiscoveryService.Services.ServiceRegistration>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var timerRequests = new TimerRequests();

app.Run();
