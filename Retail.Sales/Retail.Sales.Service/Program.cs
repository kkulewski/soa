namespace Retail.Sales.Service
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales Service";

            var endpointConfiguration = new EndpointConfiguration("retail.sales");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton(LogManager.GetLogger("Default"));

             endpointConfiguration
                .UseSerialization<NewtonsoftSerializer>();

            endpointConfiguration
                .UseTransport<RabbitMQTransport>()
                .UseDirectRoutingTopology()
                .ConnectionString("host=retail-rabbitmq");

            var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
