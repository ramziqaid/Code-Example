using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalrCoreDemoWithSqlTableDependency.EF;
using SignalrCoreDemoWithSqlTableDependency.Hubs;
using SignalrCoreDemoWithSqlTableDependency.Repository;
using SignalrCoreDemoWithSqlTableDependency.SqlTableDependencies;

namespace SignalrCoreDemoWithSqlTableDependency
{
    public class Startup
    {
        private const string ConnectionString = @"Data Source=Dev-BUILD02\SQL2017;Initial Catalog=ZRamzi_DB;User Id=sa;Password=123;";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalR();

            // dependency injection
            services.AddDbContextFactory<GaugeContext>(ConnectionString);
            services.AddSingleton<IGaugeRepository, GaugeRepository>();
            services.AddSingleton<IDatabaseSubscription, GaugeDatabaseSubscription>();
            services.AddSingleton<IHubContext<GaugeHub>, HubContext<GaugeHub>>();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<GaugeHub>("gauges");
                routes.MapHub<ChatHub>("chat");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSqlTableDependency<IDatabaseSubscription>(ConnectionString);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
