using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.DbModel
{
    public class DataAuthority :BaseModel
    {
		/// <summary>
		/// 关联用户编号
		/// </summary>
		public Guid UserID { get; set; }

		/// <summary>
		/// 关联部门编号
		/// </summary>
		public Guid DepartID { get; set; }

		/// <summary>
		/// 0 未删除 1 已删除
		/// </summary>
		public int IsDeleted { get; set; }
	}
}
