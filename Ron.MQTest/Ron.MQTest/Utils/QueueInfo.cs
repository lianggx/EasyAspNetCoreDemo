using RabbitMQ.Client.Events;
using Ron.MQTest.Helpers;
using System;

namespace Ron.MQTest.Utils
{
    public partial class QueueInfo
    {
        /// <summary>
        ///  队列名称
        /// </summary>
        public string Queue { get; set; }
        /// <summary>
        ///  路由名称
        /// </summary>
        public string RouterKey { get; set; }
        /// <summary>
        ///  交换机类型
        /// </summary>
        public string ExchangeType { get; set; }
        /// <summary>
        ///  接受消息委托
        /// </summary>
        public Action<MessageBody> OnReceived { get; set; }
        /// <summary>
        ///  输出信息到客户端
        /// </summary>
        public Action<MQChannel, MessageLevel, string> OnAction { get; set; }
    }
}
