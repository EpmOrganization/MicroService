using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.DbModel
{
    public class WorkItem : BaseModel
    {
        /// <summary>
        /// 工作记录内容
        /// </summary>
        public string WorkContent {get; set;}

        /// <summary>
        /// 工作日期
        /// </summary>
        public DateTime RecordDate {get; set;}

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreateUserID { get; set; }

        /// <summary>
        /// 0：未记录 1：已记录
        /// </summary>
        public int IsRecord { get; set; }
    }
}
