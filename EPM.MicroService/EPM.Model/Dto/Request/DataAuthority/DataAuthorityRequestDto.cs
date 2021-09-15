using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Request.DataAuthority
{
    /// <summary>
    /// 数据权限请求
    /// </summary>
    public class DataAuthorityRequestDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 选择的权限ID
        /// </summary>
        public Guid[] ParaAuthorities { get; set; }
    }
}
