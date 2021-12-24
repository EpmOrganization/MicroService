using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.LoadBalance
{
    /// <summary>
    /// 随机负载均衡算法
    /// 
    /// </summary>
    public class RandomLoadBalance : AbstractLoadBalance
    {
        private readonly Random random = new Random();

        protected override ServiceUrl DoSelect(List<ServiceUrl> serviceUrls)
        {
            // 1、获取随机数
            var index = random.Next(serviceUrls.Count);

            // 2、选择一个服务
            return serviceUrls[index];
        }
    }
}
