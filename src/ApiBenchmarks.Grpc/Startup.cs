// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Grpc
{
    using ApiBenchmarks.Grpc.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Startup chores.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures all the services.
        /// </summary>
        /// <param name="services">A collection of services to be populated.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ResponseGenerator>();
            services.AddGrpc();
        }

        /// <summary>
        /// Configures the app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BenchmarkerService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
