using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.DataAuthorityResponse
{
    /// <summary>
    /// 数据权限返回类型
    /// </summary>
    public class DataAuthorityResponseDto 
    {
        /// <summary>
        /// 选中 0 未选中 1 选中
        /// </summary>
        public int Allotted { get; set; }

        public Guid DeptId { get; set; }

        public string DeptName { get; set; }

        public List<DataAuthorityResponseDto> Children { get; set; }
    }

}
