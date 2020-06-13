namespace Retail.Recommendations.Service
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.MessageMutator;
    using Mutators;
    using Events;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Recommendations Service";

            var endpointConfiguration = new EndpointConfiguration("recommendations");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton(LogManager.GetLogger("Default"));

            endpointConfiguration
                .UseSerialization<NewtonsoftSerializer>();

            endpointConfiguration
                .RegisterMessageMutator(new CommonIncomingNamespaceMutator());

            endpointConfiguration
                .Conventions()
                .DefiningEventsAs(type => type.Namespace == typeof(OrderPlaced).Namespace);

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
