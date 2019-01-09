using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.ListenerDemo.Common
{
    public class HomeEventSource : EventSource
    {
        public static HomeEventSource Instance = new HomeEventSource();

        [Event(1001)]
        public void RequestStart(string message) => WriteEvent(1001, message);

        [Event(1002)]
        public void RequestStop(string message) => WriteEvent(1002, message);
    }
}
