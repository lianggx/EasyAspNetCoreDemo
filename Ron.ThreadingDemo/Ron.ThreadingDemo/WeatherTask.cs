using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.ThreadingDemo
{
    public class WeatherTask
    {
        public async static Task GetToday()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            HttpClient client = new HttpClient();
            var res = await client.GetAsync("http://www.weather.com.cn/data/sk/101110101.html", cts.Token);
            var result = await res.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            cts.Dispose();
            client.Dispose();
        }
    }
}
