using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.ConfigModel
{
    /// <summary>
    /// Api网关配置信息
    /// </summary>
    public class ApiGatewayConfig
    {
        /// <summary>
        /// 网关URL
        /// </summary>
        public string ApiGateWay { get; set; }

        public UserMethodConfig UserMethodConfig { get; set; }
    }
}
