using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ron.ListenerDemo.Common;

namespace Ron.ListenerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void AddEventListener(IServiceCollection services)
        {
            var listeners = this.Configuration.GetSection("listener").Get<List<ListenerItem>>();
            Dictionary<string, ListenerItem> dict = new Dictionary<string, ListenerItem>();
            if (listeners != null)
            {
                foreach (var item in listeners)
                {
                    dict.Add(item.Name, item);
                }
            }
            var report = new ReportListener(dict);
            services.AddSingleton<ReportListener>(report);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddEventListener(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
