using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.LoadBalance
{
    /// <summary>
    /// 负载均衡抽象实现
    /// </summary>
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        public ServiceUrl Select(List<ServiceUrl> serviceUrls)
        {
            if (serviceUrls == null || serviceUrls.Count == 0)
                return null;
            if (serviceUrls.Count == 1)
                return serviceUrls[0];
            return DoSelect(serviceUrls);
        }

        /// <summary>
        /// 具体的子类根据不同负载均衡算法去实现
        /// 访问修饰符是protected的
        /// </summary>
        /// <param name="serviceUrls">服务列表</param>
        /// <returns></returns>
        protected abstract ServiceUrl DoSelect(List<ServiceUrl> serviceUrls);
    }
}
