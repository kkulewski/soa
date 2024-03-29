using System.Data;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Retail.Catalog.Web
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
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            this.EnsureProductTableExists();
        }

        private void EnsureProductTableExists()
        {
            using (IDbConnection dbConnection =
                new NpgsqlConnection(this.Configuration.GetValue<string>("Db:ConnectionString")))
            {
                dbConnection.Open();
                dbConnection.Execute(
                    "CREATE TABLE IF NOT EXISTS product (productId text PRIMARY KEY, name text, description text)");
            }
        }
    }
}
