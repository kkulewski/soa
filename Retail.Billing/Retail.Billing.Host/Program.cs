namespace Retail.Billing.Host
{
    using System;
    using System.Threading.Tasks;
    using System.Threading;
    using MassTransit;
    using Retail.Billing.Host.Consumers;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing Service";

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
