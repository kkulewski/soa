namespace Retail.Billing.Host
{
    using System;
    using System.Threading.Tasks;
    using System.Threading;
    using MassTransit;
    using OpenTelemetry;
    using OpenTelemetry.Trace;
    using OpenTelemetry.Resources;
    using Retail.Billing.Host.Consumers;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing Service";

            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource("Retail.Billing")
                .AddSource("MassTransit")
                .AddJaegerExporter(o =>
                {
                    o.AgentHost = "retail-jaeger";
                    o.AgentPort = 6831;
                })
                .AddConsoleExporter()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: "Retail.Billing", serviceVersion: "1.0.0"))
                .Build();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("retail-rabbitmq");

                cfg.ReceiveEndpoint("billing", e =>
                {
                    e.Consumer<OrderPlacedConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
