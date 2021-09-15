using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EPM.Model.Enum
{
    /// <summary>
    /// 系统自定义编码
    /// </summary>
    public enum CustomerCode
    {
        [Description("操作成功")]
        Success = 1,

        [Description("操作失败")]
        Fail = 0,

        [Description("该用户已存在")]
        UserIsExist = 1001
    }
}
