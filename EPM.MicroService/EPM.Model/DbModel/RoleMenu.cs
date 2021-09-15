using EPM.Model.DbModel.Base;
using System;

namespace EPM.Model.DbModel
{
    public class RoleMenu : BaseModel
    {
		/// <summary>
		/// 角色编号
		/// </summary>
		public Guid RoleID { get; set; }

		/// <summary>
		/// 菜单编号
		/// </summary>
		public Guid MenuID { get; set; }

		/// <summary>
		/// 删除
		/// </summary>
		public int IsDeleted { get; set; }
	}
}
