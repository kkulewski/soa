

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
                    var endpointConfiguration = new EndpointConfiguration("Retail.Frontend");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();
                    var routing = transport.Routing();
                    routing.RouteToEndpoint(
                        assembly: typeof(PlaceOrder).Assembly,
                        destination: "Retail.Sales");
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
