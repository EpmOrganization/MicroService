using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response
{
    /// <summary>
    /// 返回实体基类
    /// </summary>
    public class BaseResponseDto<T> where T : class, new()
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> ResponseData { get; set; }

        /// <summary>
        /// 返回查询数据的数量
        /// </summary>
        public int Count { get; set; }
    }
}
