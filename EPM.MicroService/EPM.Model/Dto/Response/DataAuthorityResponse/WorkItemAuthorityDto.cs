using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.DataAuthorityResponse
{
    public class WorkItemAuthorityDto
    {
        ///// <summary>
        ///// 界面显示的名称
        ///// </summary>
        //public string Label { get; set; }

        ///// <summary>
        ///// 对应的值
        ///// </summary>
        //public string Value { get; set; }

        /// <summary>
        /// 界面显示的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对应的值
        /// </summary>
        public Guid Id { get; set; }


        public List<WorkItemAuthorityDto> Children { get; set; }
    }
}
