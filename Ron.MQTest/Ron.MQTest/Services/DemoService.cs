using RabbitMQ.Client;
using Ron.MQTest.Helpers;
using Ron.MQTest.Utils;
using System;

namespace Ron.MQTest.Services
{
    public class DemoService : MQServiceBase
    {
        public Action<MessageLevel, string, Exception> OnAction = null;
        public DemoService(MQConfig config) : base(config)
        {
            base.Queues.Add(new QueueInfo()
            {
                ExchangeType = ExchangeType.Direct,
                Queue = "login-message",
                RouterKey = "pk",
                OnReceived = this.OnReceived
            });
        }

        public override string vHost { get { return "gpush"; } }
        public override string Exchange { get { return "user"; } }


        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceived(MessageBody message)
        {
            try
            {
                Console.WriteLine(message.Content);
            }
            catch (Exception ex)
            {
                OnAction?.Invoke(MessageLevel.Error, ex.Message, ex);
            }
            message.Consumer.Model.BasicAck(message.BasicDeliver.DeliveryTag, true);

        }
    }
}
