using EPM.Core.Discovery;
using EPM.Core.LoadBalance;
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
        public static IServiceCollection AddHttpClientConsul(this IServiceCollection services)
        {
            // 1、注册consul服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();

            // 2、注册服务负载均衡
            services.AddSingleton<ILoadBalance, RandomLoadBalance>();

            // 3、注册httpclient
            services.AddSingleton<IConsulHttpClient, ConsulHttpClient>();

            return services;
        }
    }
}
