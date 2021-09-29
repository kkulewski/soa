namespace Retail.Recommendations.Service
{
    using MassTransit;
    using Retail.Recommendations.Service.Consumers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Recommendations Service";

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("retail-rabbitmq");

                cfg.ReceiveEndpoint("event-listener", e =>
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
