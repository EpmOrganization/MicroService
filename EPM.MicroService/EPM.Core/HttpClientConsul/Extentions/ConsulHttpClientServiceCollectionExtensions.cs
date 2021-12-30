using EPM.Core.Discovery;
using EPM.Core.LoadBalance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPM.Core.HttpClientConsul.Extentions
{
    /// <summary>
    /// HttpClientFactory结合consul服务发现进行扩展
    /// </summary>
    public static class ConsulHttpClientServiceCollectionExtensions
    {
        /// <summary>
        /// 添加consul
        /// </summary>
        /// <typeparam name="ConsulHttpClient"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientConsul(this IServiceCollection services, IConfiguration configuration)
        {
            // 1、注册consul服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();

            // 2、注册服务负载均衡
            switch(configuration.GetSection("LoadBalanceType").Value)
            {
                case "1":
                    // 随机数负载均衡
                    services.AddSingleton<ILoadBalance, RandomLoadBalance>();
                    break;
                    case "2":
                    // 轮询
                    services.AddSingleton<ILoadBalance, RoundRobinLoadBalance>();
                    break;
                default:
                    services.AddSingleton<ILoadBalance, RandomLoadBalance>();
                    break;
            }

            // 3、注册httpclient
            services.AddSingleton<IConsulHttpClient, ConsulHttpClient>();

            return services;
        }
    }
}
