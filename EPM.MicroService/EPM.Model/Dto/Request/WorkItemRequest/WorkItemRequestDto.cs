using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Request.WorkItemRequest
{
    public class WorkItemRequestDto : PagingRequest
    {
        //public int Year { get; set; }

        //public int Month { get; set; }

        /// <summary>
        /// 选择的日期    
        /// </summary>
        public string SelectedDate { get; set; }
    }
}
