using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.RoleMenuResponse
{
    public class RoleMenuResponseDto 
    {
        /// <summary>
        /// 对应角色表的自增主键
        /// </summary>
        public int ClusterID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 父级选择状态
        /// </summary>
        public string HalfCheckeds { get; set; }

        /// <summary>
        /// 角色对应的权限集合
        /// </summary>
        public List<Guid> AllottedMenus { get; set; }
    }
}
