using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Filters
{
    public class PaginationFilterRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<FilterCondition> Filters { get; set; } = new();

        public PaginationFilterRequest() { }

        public PaginationFilterRequest(int pageNumber, int pageSize, List<FilterCondition> filters = null)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
            Filters = filters ?? new List<FilterCondition>();
        }
    }
}
