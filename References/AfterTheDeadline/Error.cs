using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfterTheDeadline
{
    public class Error
    {
        public string String { get; set; }
        public string Description { get; set; }
        public string Precontext { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> Suggestions { get; set; }
    }
}
