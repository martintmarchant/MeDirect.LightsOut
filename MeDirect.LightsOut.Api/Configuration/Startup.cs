using MeDirect.LightsOut.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace MeDirect.LightsOut.Api.Configuration
{
    /// <summary>
    /// Startup Configuration Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// method adds services to the container (called by the runtime).
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureDependancies(Configuration)
                .AddControllers();
            services.AddMemoryCache();

            // Set the comments path for the Swagger.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeDirect.LightsOut.Api", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// method to configure the HTTP request pipeline (called by the runtime).
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeDirect.LightsOut.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Log.Information("Configuration Complete");
        }

    }
}