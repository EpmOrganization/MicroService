using EPM.Core.Registry;
using EPM.Model.ConfigModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPM.Core.Extentions.MicroServiceExtentions
{
    /// <summary>
    /// consule 注册中心扩展（加载配置）
    /// 对IServiceCollection进行扩展，注册到IOC容器里面
    /// </summary>
    public static class MicroServiceConsulServiceCollectionExtensions
    {
        // consul服务注册
        public static IServiceCollection AddConsulRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            
            // 1、加载Consul服务注册配置 
            // 读取配置文件中ConsulRegistry节点的配置
            services.Configure<ServiceRegistryConfig>(configuration.GetSection("ConsulRegistry"));

            // services.con
            //// 2、注册consul注册 单例生命周期
            //services.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();
            return services;
        }
    }
}
