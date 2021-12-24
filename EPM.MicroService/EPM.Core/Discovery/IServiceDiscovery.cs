using EPM.Model.ApiModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.Core.Discovery
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        Task<List<ServiceUrl>> Discovery(string serviceName);

        /// <summary>
        /// 根据服务名称获取服务列表
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        Task<List<string>> GetServicesAsync(string serviceName);
    }
}
