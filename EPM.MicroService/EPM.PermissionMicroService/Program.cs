using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EPM.PermissionMicroService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureAppConfiguration((context, builder) =>
                        {
                            var files = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Settings"), "*.settings.json");
                            foreach (var file in files)
                            {
                                builder.AddJsonFile(file, false, true);
                            }
                        }).UseStartup<Startup>();
                    });
    }
}
