using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.MenuResponse
{
   public  class MenuResponseDto : Menu
    {
        /// <summary>
        /// 子节点
        /// </summary>
        public List<MenuResponseDto> ChildrenMenu { get; set; }
    }
}
