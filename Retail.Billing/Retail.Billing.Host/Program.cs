using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.MessageMutator;
using Retail.Billing.Host.Mutators;

namespace Retail.Billing.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing Service";

            var endpointConfiguration = new EndpointConfiguration("billing");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton(LogManager.GetLogger("Default"));

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
