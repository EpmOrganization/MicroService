using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Model.ConfigModel
{
    /// <summary>
    /// 服务注册节点配置类
    /// </summary>
    public class ServiceRegistryConfig
    {
        // 服务ID
        public string ServiceId { get; set; }

        // 服务名称
        public string ServiceName { get; set; }

        //// 服务标签(版本)
        //public string[] Tags { set; get; }

        //// 服务地址(可以选填 === 默认加载启动路径)
        //public string Address { set; get; }

        //// 服务端口号(可以选填 === 默认加载启动路径端口)
        //public int Port { set; get; }

        // 服务注册地址（即向consul进行注册的consul地址）
        public string ConsulAddress { get; set; }

        // 服务健康检查地址
        public string HealthCheckAddress { get; set; }
    }
}
