using Consul;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EPM.Core.Registry
{
    /// <summary>
    /// consul服务注册实现
    /// </summary>
    public static class ConsulServiceRegistry
    {
        /// <summary>
        /// 扩展方法
        /// 注册服务（向consul注册微服务）
        /// </summary>
        /// <param name="serviceNode"></param>
        public static IApplicationBuilder UseConsulRegistry(this IApplicationBuilder app, IConfiguration configuration)
        {
            var ServiceRegistryConfig = new ServiceRegistryConfig();
            // 读取配置文件内容，并绑定到配置类上
            configuration.GetSection("ConsulRegistry").Bind(ServiceRegistryConfig);

            // 从控制台读取IP和port
            string ip = configuration["ip"];
            string port = configuration["port"];

            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(ServiceRegistryConfig.ConsulRegistryAddress);
            });

            // 2、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                // 编号，ID是唯一的，集群的时候根据编号去找到这个唯一的服务
                ID = Guid.NewGuid().ToString(),
                // 服务名字，做集群的时候根据服务名字获取所有的服务地址集合
                Name = ServiceRegistryConfig.Name,
                // 服务地址
                Address = ip,
                // 服务端口
                Port = int.Parse(port),
                Tags = new[] { ServiceRegistryConfig.Name },
                // 健康检查
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = $"https://{ip}:{port}{ServiceRegistryConfig.HealthCheckAddress}",
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 3、注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 4、获取应用程序生命周期
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 5、服务器关闭时注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
            return app;
        }
    }
}
