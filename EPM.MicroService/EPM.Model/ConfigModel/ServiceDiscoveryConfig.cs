using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Model.ConfigModel
{
    /// <summary>
    /// consul服务发现配置类
    /// </summary>
    public class ServiceDiscoveryConfig
    {
        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string RegistryAddress { set; get; }
    }
}
