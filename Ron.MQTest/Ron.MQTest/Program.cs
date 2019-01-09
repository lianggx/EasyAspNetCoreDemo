using Ron.MQTest.Helpers;
using Ron.MQTest.Services;
using Ron.MQTest.Utils;
using System;

namespace Ron.MQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static void Test()
        {
            MQConfig config = new MQConfig()
            {
                HostName = "172.16.1.219",
                Password = "123456",
                Port = 5672,
                UserName = "lgx"
            };

            MQServcieManager manager = new MQServcieManager();
            manager.AddService(new DemoService(config));
            manager.OnAction = OnActionOutput;
            manager.Start();

            Console.WriteLine("服务已启动");
            Console.ReadKey();

            manager.Stop();
            Console.WriteLine("服务已停止,按任意键退出...");
            Console.ReadKey();
        }

        static void OnActionOutput(MessageLevel level, string message, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} | {1} | {2}", level, message, ex?.StackTrace);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
