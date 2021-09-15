using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.ApiModel
{
    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class PagingRequest
    {
        /// <summary>
        /// 是否分页，默认为true
        /// </summary>
        public bool IsPaging { get; set; } = true;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页数据大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        ///// <summary>
        ///// 排序 ascending  升序  descending 降序
        ///// </summary>
        //public string Order { get; set; } = "desc";

        ///// <summary>
        ///// 排序字段
        ///// </summary>
        //public string SortField { get; set; } = "CreateTime";
    }
}
