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
                    var endpointConfiguration = new EndpointConfiguration("retail.frontend");

                    endpointConfiguration
                        .UseSerialization<NewtonsoftSerializer>();

                    var transport = endpointConfiguration
                        .UseTransport<RabbitMQTransport>()
                        .UseDirectRoutingTopology()
                        .ConnectionString("host=retail-rabbitmq");

                    transport.Routing()
                        .RouteToEndpoint(assembly: typeof(PlaceOrder).Assembly, destination: "retail.sales");

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
