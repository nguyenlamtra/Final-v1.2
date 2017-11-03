using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace COmpStore
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    BuildWebHost().Run();
        //}

        //public static IWebHost BuildWebHost()
        //{
        //    return new WebHostBuilder()
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .ConfigureAppConfiguration((builderContext, config) =>
        //        {
        //            IHostingEnvironment env = builderContext.HostingEnvironment;

        //            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
        //        })
        //        .UseIISIntegration()
        //        .UseDefaultServiceProvider((context, options) =>
        //        {
        //            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        //        })
        //        .UseStartup<Startup>()
        //        .Build();
        //}

        ////public static void Main(string[] args)
        ////{
        ////    BuildWebHost(args).Run();
        ////}

        ////public static IWebHost BuildWebHost(string[] args) =>
        ////    WebHost.CreateDefaultBuilder(args)
        ////        .UseStartup<Startup>()
        ////        .Build();

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables("ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
