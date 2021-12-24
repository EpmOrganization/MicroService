using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.LoadBalance
{
    /// <summary>
    /// 服务负载均衡
    /// 随机算法   服务访问量足够大
    /// 轮询算法   轮询算法是可以保证所有节点都能被访问到
    /// 一致性hash  同一个来源，同一个IP
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceUrls">服务列表</param>
        /// <returns></returns>
        ServiceUrl Select(List<ServiceUrl> serviceUrls);
    }
}
