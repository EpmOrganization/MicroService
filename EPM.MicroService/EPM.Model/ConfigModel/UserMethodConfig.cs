using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.ConfigModel
{
    /// <summary>
    /// 用户服务提供的方法
    /// </summary>
    public class UserMethodConfig
    {
        public string GetUser { get; set; }

        /// <summary>
        /// 测试Ocelot控制器
        /// </summary>
        public string TestOcelot { get; set; }

        public string AddUser { get; set; }
    }
}
