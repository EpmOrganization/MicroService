using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;

namespace EPM.Model.Dto.Request.WorkItemRequest
{
    public class AuthorityWorkItemRequestDto : PagingRequest
    {
        /// <summary>
        /// 选择的日期    
        /// </summary>
        public string SelectedDate { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public List<Guid> DepartID { get; set; }
    }
}
