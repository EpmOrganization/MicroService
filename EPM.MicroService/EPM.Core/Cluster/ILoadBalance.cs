using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Cluster
{
    /// <summary>
    /// 服务负载均衡
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
