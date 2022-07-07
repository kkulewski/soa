using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Retail.Sales.Consumers;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Retail.Sales
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://retail-auth";
                    options.Audience = "retail";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "retail");
                });
            });

            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("retail-rabbitmq");

                    cfg.ReceiveEndpoint("sales", e =>
                    {
                        e.Consumer<PlaceOrderConsumer>();
                        e.Consumer<OrderPaidConsumer>();
                        e.Consumer<OrderShippedConsumer>();
                    });
                });
            });

            services.AddOpenTelemetryTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .AddConsoleExporter()
                    .AddJaegerExporter(o =>
                    {
                        o.AgentHost = "retail-jaeger";
                        o.AgentPort = 6831;
                    })
                    .AddSource("Retail.Sales")
                    .AddSource("MassTransit")
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault().AddService(serviceName: "Retail.Sales", serviceVersion: "1.0.0"))
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddSqlClientInstrumentation();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllers();
                //.RequireAuthorization("ApiScope");
            });
        }
    }
}
