using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ron.OtherDB.Models;

namespace Ron.OtherDB
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
            // MariaDB/MySql 上下文初始化
            services.AddDbContext<MySqlForumContext>(options =>
            {
                var connectionString = this.Configuration["ConnectionStrings:Mysql.Forum"];
                options.UseMySql(connectionString);
            });

            // PostgreSQL 上下文初始化
            services.AddDbContext<NPgSqlForumContext>(options =>
            {
                var connectionString = this.Configuration["ConnectionStrings:Pgsql.Forum"];
                options.UseNpgsql(connectionString);
            });

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
