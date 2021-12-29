using Consul;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Registry
{
    /// <summary>
    /// consul服务注册扩展类
    /// </summary>
    public static class ConsulRegistrationExtensions
    {
        /// <summary>
        /// 扩展IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceRegistryConfig>(configuration.GetSection("ConsulRegistry"));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            // 获取主机生命周期管理接口
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 获取服务配置项
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ServiceRegistryConfig>>().Value;

            // 服务ID必须保证唯一
            serviceOptions.ServiceId=Guid.NewGuid().ToString();

            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration => 
            {
                // 1.1 建立客户端和服务端连接 服务注册的地址，集群中任意一个地址
                configuration.Address=new Uri(serviceOptions.ConsulAddress);
            });

            // 获取当前服务地址和端口
            var features = app.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            var uri=new Uri(address);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Scheme",uri.Scheme);
            // 2、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                // 编号，ID是唯一的，集群的时候根据编号去找到这个唯一的服务
                ID = serviceOptions.ServiceId,
                // 服务名称 做集群的时候根据服务名字获取所有的服务地址集合
                Name = serviceOptions.ServiceName,
                // 地址
                Address=uri.Host,
                // 端口号
                Port=uri.Port,
                // 服务检查
                Check= new AgentServiceCheck 
                {
                    // 3.1、consul健康检查超时时间
                    Timeout = TimeSpan.FromSeconds(5),
                    // 服务停止多久后注销服务  服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // consul健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceOptions.HealthCheckAddress}",
                    // consul健康检查间隔时间  10秒检查一次
                    Interval = TimeSpan.FromSeconds(10),
                },
                Meta =dict
            };

#if DEBUG
            Console.WriteLine($"Debug HealthCheckAddress:{registration.Check.HTTP}");
#endif
            // 注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 应用程序终止时，注销服务
            lifeTime.ApplicationStopping.Register(() => 
            {
               consulClient.Agent.ServiceDeregister(serviceOptions.ServiceId).Wait();
            });

            return app;
        }
    }
}
