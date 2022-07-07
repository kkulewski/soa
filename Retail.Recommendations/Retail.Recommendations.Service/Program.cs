namespace Retail.Recommendations.Service
{
    using MassTransit;
    using OpenTelemetry;
    using OpenTelemetry.Trace;
    using OpenTelemetry.Resources;
    using Retail.Recommendations.Service.Consumers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Recommendations Service";

            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource("Retail.Recommendations")
                .AddSource("MassTransit")
                .AddJaegerExporter(o =>
                {
                    o.AgentHost = "retail-jaeger";
                    o.AgentPort = 6831;
                })
                .AddConsoleExporter()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: "Retail.Recommendations", serviceVersion: "1.0.0"))
                .Build();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("retail-rabbitmq");

                cfg.ReceiveEndpoint("recommendations", e =>
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
