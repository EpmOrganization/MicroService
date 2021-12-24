using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.LoadBalance
{
    /// <summary>
    /// 轮询负载均衡算法
    /// </summary>
    public class RoundRobinLoadBalance : ILoadBalance
    {
        // 锁
        private readonly object _locker = new object();

        private int _index = 0;

        public ServiceUrl Select(List<ServiceUrl> serviceUrls)
        {
            // 使用lock控制并发
           lock (_locker)
            {
                if(_index >= serviceUrls.Count)
                {
                    _index = 0;
                }
                return serviceUrls[_index];
            }
        }
    }
}
