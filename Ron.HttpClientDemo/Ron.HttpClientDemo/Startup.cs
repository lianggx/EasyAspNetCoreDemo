using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System.Net.WebSockets;
using Polly.Retry;
using Polly.Extensions.Http;

namespace Ron.HttpClientDemo
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
            services.AddHttpClient();

            services.AddHttpClient<WeatherService>();

            services.AddHttpClient<WeatherService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddPolicyHandler(policy =>
                    {
                        return HttpPolicyExtensions.HandleTransientHttpError()
                                                   .WaitAndRetryAsync(3,
                                                   retryAttempt => TimeSpan.FromSeconds(2),
                                                   (exception, timeSpan, retryCount, context) =>
                                                   {
                                                       Console.ForegroundColor = ConsoleColor.Yellow;
                                                       Console.WriteLine("请求出错了：{0} | {1} ", timeSpan, retryCount);
                                                       Console.ForegroundColor = ConsoleColor.Gray;
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
