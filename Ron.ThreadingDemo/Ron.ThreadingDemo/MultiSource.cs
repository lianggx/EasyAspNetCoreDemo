using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.ThreadingDemo
{
    public class MultiSource
    {
        public static void Test()
        {
            CancellationTokenSource cts1 = new CancellationTokenSource();
            cts1.Token.Register(() =>
            {
                Console.WriteLine("\ncts1 ThreadId： {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            });
            cts1.Cancel();
            Console.WriteLine("cts1 State：{0}", cts1.IsCancellationRequested);

            CancellationTokenSource cts2 = new CancellationTokenSource();
            cts2.Token.Register(() =>
            {
                Console.WriteLine("\ncts2 ThreadId： {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            });
            cts2.CancelAfter(500);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("cts2 State：{0}", cts2.IsCancellationRequested);

            CancellationTokenSource cts3 = new CancellationTokenSource();
            cts3.Token.Register(() =>
            {
                Console.WriteLine("\ncts3 ThreadId： {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            });
            cts3.Dispose();
            Console.WriteLine("\ncts3 State：{0}", cts3.IsCancellationRequested);
        }
    }
}
