using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.ListenerDemo.Common
{
    public class ListenerItem
    {
        public string Name { get; set; }
        public EventLevel Level { get; set; } = EventLevel.Verbose;
        public EventKeywords Keywords { get; set; } = EventKeywords.All;
    }
}
