using Ron.MQTest.Helpers;
using System.Collections.Generic;

namespace Ron.MQTest.Utils
{
    public abstract class MQServiceBase : IService
    {
        internal bool started = false;
        internal MQServiceBase(MQConfig config)
        {
            this.Config = config;
        }

        public MQChannel CreateChannel(string queue, string routeKey, string exchangeType)
        {
            MQConnection conn = new MQConnection(this.Config, this.vHost);
            MQChannelManager cm = new MQChannelManager(conn);
            MQChannel channel = cm.CreateReceiveChannel(exchangeType, this.Exchange, queue, routeKey);
            return channel;
        }

        /// <summary>
        ///  启动订阅
        /// </summary>
        public void Start()
        {
            if (started)
            {
                return;
            }

            MQConnection conn = new MQConnection(this.Config, this.vHost);
            MQChannelManager manager = new MQChannelManager(conn);
            foreach (var item in this.Queues)
            {
                MQChannel channel = manager.CreateReceiveChannel(item.ExchangeType, this.Exchange, item.Queue, item.RouterKey);
                channel.OnReceivedCallback = item.OnReceived;
                this.Channels.Add(channel);
            }
            started = true;
        }

        /// <summary>
        ///  停止订阅
        /// </summary>
        public void Stop()
        {
            foreach (var c in this.Channels)
            {
                c.Stop();
            }
            this.Channels.Clear();
            started = false;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        public abstract void OnReceived(MessageBody message);

        public List<MQChannel> Channels { get; set; } = new List<MQChannel>();

        /// <summary>
        ///  消息队列配置
        /// </summary>
        public MQConfig Config { get; set; }

        /// <summary>
        ///  消息队列中定义的虚拟机
        /// </summary>
        public abstract string vHost { get; }

        /// <summary>
        ///  消息队列中定义的交换机
        /// </summary>
        public abstract string Exchange { get; }

        /// <summary>
        ///  定义的队列列表
        /// </summary>
        public List<QueueInfo> Queues { get; } = new List<QueueInfo>();
    }
}
