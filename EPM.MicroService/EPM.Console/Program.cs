using EPM.Core.Discovery;
using EPM.Core.ServiceBase;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EPM.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 读取配置文件
            var files = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Settings"), "*.settings.json");

            var build = new ConfigurationBuilder();
            foreach (var file in files)
            {
                build.AddJsonFile(file, false, true);
            }

            IConfiguration configuration= build.Build();

            // 依赖注入
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.Configure<ServiceDiscoveryConfig>(configuration.GetSection("ConsulDiscovery"));

            services.AddScoped<IServiceDiscovery, ConsulServiceDiscovery>();

            ServiceFactory.Services = services;

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                //TestServiceImpl testService=serviceProvider.GetService<TestServiceImpl>();  

                var discover = serviceProvider.GetService<IServiceDiscovery>();
                List<ServiceUrl> list = await  discover.Discovery("UserService");
                foreach (ServiceUrl url in list)
                {
                    System.Console.WriteLine(url.Url);
                }

            }

            System.Console.WriteLine("Hello World!");

            System.Console.ReadKey();
        }
    }
}
