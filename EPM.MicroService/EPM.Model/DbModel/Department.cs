using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.DbModel
{
    public class Department : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级组编号
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 是否删除 0：正常存在 1：被删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 记录选择的父级
        /// </summary>
        public string Dep { get; set; }
    }
}
