using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.DbModel
{
    public class Menu : BaseModel
	{
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 菜单对应路由
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// 父级权限编号
		/// </summary>
		public Guid? ParentMenuID { get; set; }

		/// <summary>
		/// 类型
		/// </summary>
		public int? Type { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public int IsDeleted { get; set; }

		/// <summary>
		/// 父级
		/// </summary>
		public string ParentList { get; set; }
	}
}
