using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.BackHost.Common
{
    public class BackManagerService : BackgroundService
    {
        BackManagerOptions options = new BackManagerOptions();
        public BackManagerService(Action<BackManagerOptions> options)
        {
            options.Invoke(this.options);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 延迟启动
            await Task.Delay(this.options.CheckTime, stoppingToken);

            options.OnHandler(0, $"正在启动托管服务 [{this.options.Name}]....");
            stoppingToken.Register(() =>
            {
                options.OnHandler(1, $"托管服务  [{this.options.Name}] 已经停止");
            });

            int count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                count++;
                options.OnHandler(1, $" [{this.options.Name}] 第 {count} 次执行任务....");
                try
                {
                    options?.Callback();
                    if (count == 3)
                        throw new Exception("模拟业务报错");
                }
                catch (Exception ex)
                {
                    options.OnHandler(2, $" [{this.options.Name}] 执行托管服务出错", ex);
                }
                await Task.Delay(this.options.CheckTime, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            options.OnHandler(3, $" [{this.options.Name}] 由于进程退出，正在执行清理工作");
            return base.StopAsync(cancellationToken);
        }
    }
}
