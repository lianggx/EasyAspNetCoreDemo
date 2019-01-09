using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.ListenerDemo.Common
{
    public class ReportListener : EventListener
    {
        public ReportListener() { }

        public Dictionary<string, ListenerItem> Items { get; set; } = new Dictionary<string, ListenerItem>();
        public ReportListener(Dictionary<string, ListenerItem> items)
        {
            this.Items = items;
        }
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name.Equals("Microsoft-Windows-DotNETRuntime"))
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.AuditFailure);
            }

            else if (eventSource.Name.Equals("System.Data.DataCommonEventSource"))
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.AuditFailure);
            }

            else if (eventSource.Name.Equals("Microsoft-AspNetCore-Server-Kestrel"))
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.AuditFailure);
            }

            else if (Items.ContainsKey(eventSource.Name))
            {
                var item = Items[eventSource.Name];
                EnableEvents(eventSource, item.Level, item.Keywords);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (Items.ContainsKey(eventData.EventSource.Name))
            {
                Console.WriteLine($"ThreadID = {eventData.OSThreadId} ID = {eventData.EventId} Name = {eventData.EventSource.Name}.{eventData.EventName}");
                for (int i = 0; i < eventData.Payload.Count; i++)
                {
                    string payloadString = eventData.Payload[i]?.ToString() ?? string.Empty;
                    Console.WriteLine($"\tName = \"{eventData.PayloadNames[i]}\" Value = \"{payloadString}\"");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
