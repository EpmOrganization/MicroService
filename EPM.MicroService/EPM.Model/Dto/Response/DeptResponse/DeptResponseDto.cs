using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.DeptResponse
{
    public class DeptResponseDto: Department
    {
        /// <summary>
        /// 子级
        /// </summary>
        public List<DeptResponseDto> Children { get; set; }
    }
}
