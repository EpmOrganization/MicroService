using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Model.ConfigModel
{
    /// <summary>
    /// 用户服务配置信息
    /// </summary>
    public class UserServiceConfig
    {
        public string Scheme { get; set; }

        public string ServiceName { get; set; }

        public string GetUri { get; set; }
    }
}
