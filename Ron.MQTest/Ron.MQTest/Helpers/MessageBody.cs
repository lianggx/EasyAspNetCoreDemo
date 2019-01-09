using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.MQTest.Helpers
{
    public class MessageBody
    {
        public EventingBasicConsumer Consumer { get; set; }
        public BasicDeliverEventArgs BasicDeliver { get; set; }
        /// <summary>
        ///  0成功
        /// </summary>
        public int Code { get; set; }
        public string Content { get; set; }
        public string ErrorMessage { get; set; }
        public bool Error { get; set; }
        public Exception Exception { get; set; }
    }
}
