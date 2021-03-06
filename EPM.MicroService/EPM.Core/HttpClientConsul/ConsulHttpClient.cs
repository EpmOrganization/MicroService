using EPM.Core.Discovery;
using EPM.Core.LoadBalance;
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
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务路径</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<T> GetAsync<T>(string serviceName, string serviceLink)
        {
            // 1、通过consul服务发现获取所有的服务地址
            List<ServiceUrl> serviceUrls = await serviceDiscovery.Discovery(serviceName);

            // 2、负载均衡服务
            ServiceUrl serviceUrl = loadBalance.Select(serviceUrls);

            // 3、建立httpclient请求
            HttpClient httpClient = httpClientFactory.CreateClient("mrico");
            HttpResponseMessage response = await httpClient.GetAsync(serviceUrl.Url + serviceLink);

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

        /// <summary>
        /// Get方法获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<T> GetByGatewayAsync<T>(string url)
        {

            // 3、建立httpclient请求
            HttpClient httpClient = httpClientFactory.CreateClient("mrico");
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // 3.1json转换成对象
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                throw new Exception($"服务调用错误");
            }
        }
    }
}
