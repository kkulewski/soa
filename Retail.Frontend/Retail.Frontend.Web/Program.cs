

namespace Retail.Frontend.Web
{
    using Messages;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(c =>
                {
                    var endpointConfiguration = new EndpointConfiguration("frontend");

                    var transport = endpointConfiguration
                        .UseTransport<RabbitMQTransport>()
                        .UseDirectRoutingTopology()
                        .ConnectionString("host=localhost");

                    transport.Routing()
                        .RouteToEndpoint(assembly: typeof(PlaceOrder).Assembly, destination: "sales");

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}
