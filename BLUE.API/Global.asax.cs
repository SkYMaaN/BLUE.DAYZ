using BLUE.API.Abstractions;
using BLUE.API.Controllers;
using BLUE.API.Helpers;
using BLUE.API.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BLUE.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureLogger();

            RegisterDepencyInjection();

            SteamClientService.InitializeSteamClient();
            DayZServersMonitorService.Instance.StartMonitoring();

        }

        private void RegisterDepencyInjection()
        {
            var services = new ServiceCollection();

            services.AddScoped<DatabaseContext>();
            services.AddScoped<IDayZServerRepository, DayZServerRepository>();
            services.AddScoped<DayZServerController>();

            // Register all your controllers and other services here:
            //services.AddTransient<ValuesController>();

            var provider = services.BuildServiceProvider(new ServiceProviderOptions
            {
                // Prefer to keep validation on at all times
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new DependencyInjectionHttpControllerActivator(provider));
        }

        private void ConfigureLogger()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    Path.Combine(logDirectory, "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 10,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .CreateLogger();
        }
    }
}
