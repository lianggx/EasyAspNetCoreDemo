using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ron.BackHost.Common;

namespace Ron.BackHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BackManagerService>(factory =>
            {
                OrderManagerService order = new OrderManagerService();
                return new BackManagerService(options =>
                 {
                     options.Name = "订单超时检查";
                     options.CheckTime = 5 * 1000;
                     options.Callback = order.CheckOrder;
                     options.Handler = order.OnBackHandler;
                 });
            });

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BackManagerService>(factory =>
            {
                OrderManagerService order = new OrderManagerService();
                return new BackManagerService(options =>
                {
                    options.Name = "成交数量统计";
                    options.CheckTime = 2 * 1000;
                    options.Callback = order.CheckOrder;
                    options.Handler = order.OnBackHandler;
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
