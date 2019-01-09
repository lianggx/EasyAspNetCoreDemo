using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.ThreadingDemo
{
    public class LinkedTask
    {
        public async static Task Test()
        {
            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationTokenSource cts2 = new CancellationTokenSource();
            var cts3 = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);

            cts1.Token.Register(() =>
            {
                Console.WriteLine("cts1 Canceling");
            });
            cts2.Token.Register(() =>
            {
                Console.WriteLine("cts2 Canceling");
            });
            cts2.CancelAfter(1000);

            cts3.Token.Register(() =>
                        {
                            Console.WriteLine("cts3 Canceling");
                        });

            var res = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", cts1.Token);
            var result = await res.Content.ReadAsStringAsync();
            Console.WriteLine("cts1:{0}", result);

            var res2 = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", cts2.Token);
            var result2 = await res2.Content.ReadAsStringAsync();
            Console.WriteLine("cts2:{0}", result2);

            var res3 = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", cts3.Token);
            var result3 = await res2.Content.ReadAsStringAsync();
            Console.WriteLine("cts3:{0}", result3);
        }
    }
}
