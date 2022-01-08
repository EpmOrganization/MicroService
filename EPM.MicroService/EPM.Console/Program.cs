using EPM.Core.Discovery;
using EPM.Core.LoadBalance;
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

            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
            // 使用轮询实现负载均衡
            //services.AddSingleton<ILoadBalance, RoundRobinLoadBalance>();
            // 随机数
            services.AddSingleton<ILoadBalance, RandomLoadBalance>();

            ServiceFactory.Services = services;

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                //TestServiceImpl testService=serviceProvider.GetService<TestServiceImpl>();  

                var discover = serviceProvider.GetService<IServiceDiscovery>();
                List<ServiceUrl> list = await  discover.Discovery("UserService");
                // 测试负载均衡
                ILoadBalance loadBalance = serviceProvider.GetService<ILoadBalance>();
                for (int i = 0; i < 20; i++)
                {
                    var url = loadBalance.Select(list);
                    System.Console.WriteLine($"current url is:{url.Url}");
                }


            }

            System.Console.WriteLine("Hello World!");

            System.Console.ReadKey();
        }
    }
}
