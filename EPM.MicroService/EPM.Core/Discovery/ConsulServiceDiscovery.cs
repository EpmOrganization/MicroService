using Consul;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Discovery
{
    /// <summary>
    /// consul服务发现实现
    /// 如果是其它服务发现，只需要实现IServiceDiscovery接口即可
    /// </summary>
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly IConfiguration _configuration;

        public ConsulServiceDiscovery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ServiceUrl>> Discovery(string serviceName)
        {

            var file = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Settings"), "service.discover.json");
            // 读取配置文件
            var config = new ConfigurationBuilder().AddJsonFile(file[0], false, true).Build();

            // 绑定配置信息
            ServiceDiscoveryConfig serviceDiscoveryConfig = new ServiceDiscoveryConfig();
            config.GetSection("ConsulDiscovery").Bind(serviceDiscoveryConfig);

            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscoveryConfig.RegistryAddress);
            });

            //// 2、consul查询服务,根据具体的服务名称查询
            //var queryResult = await consulClient.Catalog.Service(serviceName);

            // Health 当前consul里已经注册的服务，健康检查的信息也获取
            var queryResult = await consulClient.Health.Service(service: serviceName,"",passingOnly: true);
            // 3、将服务进行拼接
            var list = new List<ServiceUrl>();
            foreach (var service in queryResult.Response)
            {
                list.Add(new ServiceUrl { Url = service.Service.Address + ":" + service.Service.Port });
            }
            return list;
        }

        public Task<List<string>> GetServicesAsync(string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}
