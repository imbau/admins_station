using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using admins_station.Models;
using NJsonSchema;
using NSwag.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace admins_station
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            //初始化MyOwnModel实例并且映射appSettings里的配置
            services.AddOptions();
            services.Configure<EnvironmentVariable>(Configuration.GetSection("EnvironmentVariable"));     

            // Add framework services. 
            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(MemberContext), new MemberContext()));
            services.Add(new ServiceDescriptor(typeof(calculate_rents_amt), new calculate_rents_amt()));
            services.Add(new ServiceDescriptor(typeof(staffContext), new staffContext()));
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseStaticFiles();
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
            app.UseMvc();

        }
    }
}