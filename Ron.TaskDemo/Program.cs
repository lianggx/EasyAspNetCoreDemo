using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            WithTask();
            Console.WriteLine("press any key to cancel...");
            Console.ReadKey();
        }

        static void WithTask()
        {
            var order1 = Task.Run(() =>
            {
                Console.WriteLine("Order 1");
            });

            // 匿名委托将等待 order1 执行完成后执行，并将 order1 对象作为参数传入
            order1.ContinueWith((task) =>
            {
                Console.WriteLine("Order 1 Is Completed");
            });

            var t1 = Task.Run(() => { Task.Delay(1500).Wait(); Console.WriteLine("t1"); });
            var t2 = Task.Run(() => { Task.Delay(2000).Wait(); Console.WriteLine("t2"); });
            var t3 = Task.Run(() => { Task.Delay(3000).Wait(); Console.WriteLine("t3"); });
            Task.WaitAll(t1, t2, t3);
            // t1,t2,t3 完成后输出下面的消息
            Console.WriteLine("t1,t2,t3 Is Complete");

            var t4 = Task.Run(() => { Task.Delay(1500).Wait(); Console.WriteLine("t4"); });
            var t5 = Task.Run(() => { Task.Delay(2000).Wait(); Console.WriteLine("t5"); });
            var t6 = Task.Run(() => { Task.Delay(3000).Wait(); Console.WriteLine("t6"); });
            Task.WaitAny(t4, t5, t6);
            // 当任意任务完成时，输出下面的消息，目前按延迟时间计算，在 t4 完成后立即输出下面的信息
            Console.WriteLine("t4,t5,t6 Is Complete");

            var t7 = Task.Run(() => { Task.Delay(1500).Wait(); Console.WriteLine("t7"); });
            var t8 = Task.Run(() => { Task.Delay(2000).Wait(); Console.WriteLine("t8"); });
            var t9 = Task.Run(() => { Task.Delay(3000).Wait(); Console.WriteLine("t9"); });
            var whenAll = Task.WhenAll(t7, t8, t9);
            // WhenAll 不会等待，所以这里必须显示指定等待
            whenAll.Wait();
            // 当所有任务完成时，输出下面的消息
            Console.WriteLine("t7,t8,t9 Is Complete");

            var t10 = Task.Run(() => { Task.Delay(1500).Wait(); Console.WriteLine("t10"); });
            var t11 = Task.Run(() => { Task.Delay(2000).Wait(); Console.WriteLine("t11"); });
            var t12 = Task.Run(() => { Task.Delay(3000).Wait(); Console.WriteLine("t12"); });
            var whenAny = Task.WhenAll(t10, t11, t12);
            // whenAny 不会等待，所以这里必须显示指定等待
            whenAny.Wait();
            // 当任意任务完成时，输出下面的消息，目前按延迟时间计算，在 t10 完成后立即输出下面的信息
            Console.WriteLine("t10,t11,t12 Is Complete");
        }

        static void LongTask()
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("LongRunning Task");
            }, TaskCreationOptions.LongRunning);
        }
        static void SetThreadPool()
        {
            var available = ThreadPool.SetMaxThreads(8, 16);
            Console.WriteLine("Result:{0}", available);
        }

        static void EasyTask()
        {
            // 执行一个无返回值的任务
            Task.Run(() =>
            {
                Console.WriteLine("runing...");
            });

            // 执行一个返回 int 类型结果的任务
            Task.Run<int>(() =>
            {
                return new Random().Next();
            });

            // 声明一个任务，仅声明，不执行
            Task t = new Task(() =>
            {
                Console.WriteLine("");
            });
        }

        static void SimpleTask()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var task = Task.Run(() =>
            {
                Console.WriteLine("SimpleTask");
                Task.Delay(2000);
                // throw new Exception("SimpleTask Error");
            }, cts.Token);

            try
            {
                //  cts.Cancel();
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (task.IsCompletedSuccessfully)
            {
                Console.WriteLine("IsCompleted");
            }
        }

        static void Factory()
        {
            List<Task<int>> tasks = new List<Task<int>>();
            TaskFactory factory = new TaskFactory();
            tasks.Add(factory.StartNew<int>(() =>
           {
               return 1;
           }));
            tasks.Add(factory.StartNew<int>(() =>
            {
                return 2;
            }));

            foreach (var t in tasks)
            {
                Console.WriteLine("Task:{0}", t.Result);
            }
        }

        static void TaskSynchronizationContext()
        {
            var UISyncContext = TaskScheduler.FromCurrentSynchronizationContext();

            var t1 = Task.Factory.StartNew<int>(() =>
               {
                   return 1;
               });
            t1.ContinueWith((atnt) =>
            {
                // 从这里访问 UI 线程的资源
                Console.WriteLine("从这里访问 UI 线程的资源");

            }, UISyncContext);
        }
    }
}
