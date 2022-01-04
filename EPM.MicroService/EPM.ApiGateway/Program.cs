using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureAppConfiguration((hostingContext, config) => 
            //{
            //    config.AddOcelot()
            //             .AddEnvironmentVariables();
            //})
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.ConfigureAppConfiguration((context, builder) =>
                  {
                      var files = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Settings"), "ocelot.*.json");
                      foreach (var file in files)
                      {
                          builder.AddJsonFile(file, false, true);
                      }
                   
                  }).UseStartup<Startup>();
              });
    }
}
