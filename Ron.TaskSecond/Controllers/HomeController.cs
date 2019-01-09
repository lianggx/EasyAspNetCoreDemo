using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ron.TaskSecond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            string result = string.Empty;
            await Task.Run(() =>
             {
                 result = "Hello World!";
             });
            return result;
        }

        // GET api/values/5
        [HttpGet("WaitTest")]
        public string WaitTest(int id)
        {
            string result = string.Empty;
            var waitTask = Task.Run(() =>
             {
                 result = "Hello World!";
             });
            waitTask.Wait(TimeSpan.FromSeconds(5));

            return result;
        }

        [HttpGet("WaitToken")]
        public string WaitToken(int id)
        {
            var result = string.Empty;
            CancellationTokenSource cts = new CancellationTokenSource();
            var taskToken = Task.Run(() =>
            {
                cts.CancelAfter(TimeSpan.FromSeconds(1));
                Task.Delay(2000).Wait();
                result = "Hello World!";
            });
            taskToken.Wait(cts.Token);

            return result;
        }

        // POST api/values
        [HttpGet("TaskQueue")]
        public bool TaskQueue()
        {
            var inQueues = ThreadPool.QueueUserWorkItem(ThreadProc);

            var inQueuesSecond = ThreadPool.QueueUserWorkItem(ThreadProc, "这是一条测试消息");

            return inQueues;
        }

        private void ThreadProc(Object stateInfo)
        {
            Console.WriteLine("此任务来自线程池队列执行");
        }

        [HttpGet("HyBrid")]
        public Task<string> HyBrid([FromQuery]string value)
        {
            return  HyBridInternal(value);
        }

        private async Task<string> HyBridInternal(string value)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(value);
            });

            return "Your Input:" + value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
