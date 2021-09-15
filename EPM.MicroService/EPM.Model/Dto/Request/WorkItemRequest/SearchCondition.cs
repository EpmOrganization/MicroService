using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Request.WorkItemRequest
{
    public class SearchCondition : PagingRequest
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public Guid? UserID { get; set; }
    }
}
