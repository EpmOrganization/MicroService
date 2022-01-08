using Microsoft.AspNetCore.Http;
using Ocelot.Errors;
using Ocelot.LoadBalancer.LoadBalancers;
using Ocelot.Middleware;
using Ocelot.Responses;
using Ocelot.Values;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.ApiGateway.OcelotExtension
{
    /// <summary>
    /// 自定义随机数负载均衡策略
    /// </summary>
    public class CustomRandomLoadBalancer : ILoadBalancer
    {
        private readonly Func<Task<List<Service>>> _services;
        private readonly object _lock = new object();

        private int _index;
        public CustomRandomLoadBalancer()
        {

        }
        public CustomRandomLoadBalancer(Func<Task<List<Service>>> services)
        {
            _services = services;
        }

        public async Task<Response<ServiceHostAndPort>> Lease(DownstreamContext context)
        {
            var serviceList = await _services();

            if (serviceList == null)
                return new ErrorResponse<ServiceHostAndPort>(new ServicesAreEmptyError(" Random LoadBalancer Is Error"));
            lock (_lock)
                {
                    if (serviceList.Count == 1)
                        return new OkResponse<ServiceHostAndPort>(serviceList[0].HostAndPort);
                    _index = new Random().Next(serviceList.Count);
                    return new OkResponse<ServiceHostAndPort>(serviceList[_index].HostAndPort);
                }
        }

        public void Release(ServiceHostAndPort hostAndPort)
        {

        }
    }
}
