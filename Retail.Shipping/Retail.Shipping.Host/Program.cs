namespace Retail.Shipping.Host
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.MessageMutator;
    using Mutators;
    using Repositories;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Shipping Service";

            var endpointConfiguration = new EndpointConfiguration("shipping");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton(LogManager.GetLogger("Default"));
            containerSettings.ServiceCollection.AddTransient<IOrderRepository, OrderRepository>();

            endpointConfiguration
                .UseSerialization<NewtonsoftSerializer>();

            endpointConfiguration
                .RegisterMessageMutator(new CommonIncomingNamespaceMutator());

            endpointConfiguration
                .RegisterMessageMutator(new CommonOutgoingNamespaceMutator());

            endpointConfiguration
                .UseTransport<RabbitMQTransport>()
                .UseDirectRoutingTopology(messageType => messageType.Name)
                .UsePublisherConfirms(true)
                .ConnectionString("host=retail-rabbitmq");

            var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
