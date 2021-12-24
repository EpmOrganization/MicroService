using EPM.Core.Discovery;
using EPM.Model.ApiModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddScoped<IServiceDiscovery, ConsulServiceDiscovery>();

            IConfiguration configuration=new  ConfigurationBuilder().Build();

            services.AddSingleton(configuration);

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
