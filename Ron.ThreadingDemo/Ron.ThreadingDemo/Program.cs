using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Ron.ThreadingDemo
{
    class Program
    {
        static void Main()
        {
            // 合并请求
            // ArticleTask.Test();

            ///  中断请求
            /*Task.Run(async () =>
            {
                try
                {
                    await WeatherTask.GetToday();
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });*/

            // 链式反应
            /*Task.Run(async () =>
            {
                try
                {
                    await LinkedTask.Test();
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });*/

            Console.WriteLine("Main ThreadId：{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            // 取消方式
            MultiSource.Test();


            Console.WriteLine("press any key to continue...");

            Console.ReadKey();
        }



        static void CancellBack()
        {
            Console.WriteLine("working {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        }
    }
}