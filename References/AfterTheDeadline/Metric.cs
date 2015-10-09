using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfterTheDeadline
{
    public class Metric
    {
        public MetricType Type { get; set; }
        public MetricKey Key { get; set; }
        public int Value { get; set; }
    }
}
