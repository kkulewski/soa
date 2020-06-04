namespace Retail.Frontend.Web
{
    using Messages;
    using Mutators;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;
    using NServiceBus.MessageMutator;

    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(c =>
                {
                    var endpointConfiguration = new EndpointConfiguration("frontend");

                    endpointConfiguration
                        .UseSerialization<NewtonsoftSerializer>();

                    endpointConfiguration
                        .RegisterMessageMutator(new CommonNamespaceMutator());

                    var transport = endpointConfiguration
                        .UseTransport<RabbitMQTransport>()
                        .UseDirectRoutingTopology()
                        .ConnectionString("host=retail-rabbitmq");

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
