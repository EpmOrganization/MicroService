using EPM.Core.Cluster;
using EPM.Core.Discovery;
using EPM.Model.ApiModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPM.Core.HttpClientConsul
{
    /// <summary>
    /// consul httpclient扩展
    /// </summary>
    public class ConsulHttpClient : IConsulHttpClient
    {
        private readonly IServiceDiscovery serviceDiscovery;
        private readonly ILoadBalance loadBalance;
        private readonly IHttpClientFactory httpClientFactory;
        public ConsulHttpClient(IServiceDiscovery serviceDiscovery,
                                    ILoadBalance loadBalance,
                                    IHttpClientFactory httpClientFactory)
        {
            this.serviceDiscovery = serviceDiscovery;
            this.loadBalance = loadBalance;
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// param name="ServiceSchme">Scheme:(http/https)</param>
        /// <param name="ServiceName">服务名称</param>
        /// <param name="serviceLink">服务路径</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string serviceScheme, string serviceName, string serviceLink)
        {
            // 1、通过consul服务发现获取所有的服务地址
            List<ServiceUrl> serviceUrls = await serviceDiscovery.Discovery(serviceName);

            // 2、负载均衡服务
            ServiceUrl serviceUrl = loadBalance.Select(serviceUrls);

            // 3、建立请求
            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(serviceScheme + "://" + serviceUrl.Url + serviceLink);

            // 3.1json转换成对象
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                throw new Exception($"{serviceName}服务调用错误");
            }
        }
    }
}
