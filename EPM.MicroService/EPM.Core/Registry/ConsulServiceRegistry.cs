using Consul;
using EPM.Model.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Registry
{
    /// <summary>
    /// consul服务注册实现
    /// </summary>
    public class ConsulServiceRegistry : IServiceRegistry
    {
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceNode"></param>
        public void Deregister(ServiceRegistryConfig serviceNode)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceNode.ConsulRegistryAddress);
            });

            // 2、注销服务
            consulClient.Agent.ServiceDeregister(serviceNode.Id);

            // 3、关闭连接
            consulClient.Dispose();
        }

        /// <summary>
        /// 注册服务（向consul注册微服务）
        /// </summary>
        /// <param name="serviceNode"></param>
        public void Register(ServiceRegistryConfig serviceNode)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration => 
            {
                // 1.1、建立客户端和服务端连接
               configuration.Address=new Uri(serviceNode.ConsulRegistryAddress);
            });

            // 2、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = serviceNode.Id,
                // 服务名称
                Name = serviceNode.Name,
                // 服务地址
                Address = serviceNode.Address,
                // 服务端口号
                Port = serviceNode.Port,
                Tags = serviceNode.Tags,
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = serviceNode.HealthCheckAddress,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 3、向consul注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 4、关闭连接
            consulClient.Dispose();
        }
    }
}
