using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPM.Model.DbModel
{
    public class Role :  BaseModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否删除 0：未删除 1：删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 父级半选状态
        /// </summary>
        public string HalfCheckeds { get; set; }

        /// <summary>
        /// 这个值是从前端带过来的，表示创建的角色所拥有的菜单id集合
        /// </summary>
        [NotMapped]
        public Guid[] AllottedMenus { get; set; }
    }
}
