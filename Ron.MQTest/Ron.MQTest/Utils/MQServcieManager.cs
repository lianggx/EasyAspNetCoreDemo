using System;
using System.Collections.Generic;
using System.Threading;

namespace Ron.MQTest.Utils
{
    public class MQServcieManager
    {
        public int Timer_tick { get; set; } = 10 * 1000;
        private Timer timer = null;

        public Action<MessageLevel, string, Exception> OnAction = null;
        public MQServcieManager()
        {
            timer = new Timer(OnInterval, "", Timer_tick, Timer_tick);
        }


        /// <summary>
        ///  自检，配合 RabbitMQ 内部自动重连机制
        /// </summary>
        /// <param name="sender"></param>
        private void OnInterval(object sender)
        {
            int error = 0, reconnect = 0;
            OnAction?.Invoke(MessageLevel.Information, $"{DateTime.Now} 正在执行自检", null);
            foreach (var item in this.Services)
            {
                for (int i = 0; i < item.Channels.Count; i++)
                {
                    var c = item.Channels[i];
                    if (c.Connection == null || !c.Connection.IsOpen)
                    {
                        error++;
                        OnAction?.Invoke(MessageLevel.Information, $"{c.ExchangeName} {c.QueueName} {c.RoutekeyName} 重新创建订阅", null);
                        try
                        {
                            c.Stop();
                            var channel = item.CreateChannel(c.QueueName, c.RoutekeyName, c.ExchangeTypeName);
                            item.Channels.Remove(c);
                            item.Channels.Add(channel);

                            OnAction?.Invoke(MessageLevel.Information, $"{c.ExchangeName} {c.QueueName} {c.RoutekeyName} 重新创建完成", null);
                            reconnect++;
                        }
                        catch (Exception ex)
                        {
                            OnAction?.Invoke(MessageLevel.Information, ex.Message, ex);
                        }
                    }
                }
            }
            OnAction?.Invoke(MessageLevel.Information, $"{DateTime.Now} 自检完成，错误数：{error}，重连成功数：{reconnect}", null);
        }

        public void Start()
        {
            foreach (var item in this.Services)
            {
                try
                {
                    item.Start();
                }
                catch (Exception e)
                {
                    OnAction?.Invoke(MessageLevel.Error, $"启动服务出错 | {e.Message}", e);
                }
            }
        }

        public void Stop()
        {
            try
            {
                foreach (var item in this.Services)
                {
                    item.Stop();
                }
                Services.Clear();
                timer.Dispose();
            }
            catch (Exception e)
            {
                OnAction?.Invoke(MessageLevel.Error, $"停止服务出错 | {e.Message}", e);
            }
        }


        public void AddService(IService service)
        {
            Services.Add(service);
        }
        public List<IService> Services { get; set; } = new List<IService>();
    }
}
