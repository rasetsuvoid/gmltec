using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Filters
{
    public class FilterCondition
    {
        public string? Property { get; set; }
        public object? Value { get; set; }
        public string? Operator { get; set; }
        public string? LogicalOperator { get; set; } = "AND";
    }
}
