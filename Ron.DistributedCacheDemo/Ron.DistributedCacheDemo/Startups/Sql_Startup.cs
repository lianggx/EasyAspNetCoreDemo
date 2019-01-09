using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ron.DistributedCacheDemo.Startups.SqlServer
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
            services.AddDistributedSqlServerCache(options =>
            {
                options.SystemClock = new BLL.LocalSystemClock();
                options.ConnectionString = this.Configuration["ConnectionString"];
                options.SchemaName = "dbo";
                options.TableName = "AspNetCoreCache";
                options.DefaultSlidingExpiration = TimeSpan.FromMinutes(1);
                options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
