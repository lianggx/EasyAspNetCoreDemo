using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ron.SwaggerTest
{
    public class Startup
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.Env = env;
        }

        public IHostingEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            if (Env.IsDevelopment())
            {
                services.AddSwaggerGen(options =>
               {
                   foreach (var d in docs) options.SwaggerDoc(d, new Info { Version = d });
                   options.DocInclusionPredicate((docName, description) =>
                   {
                       description.TryGetMethodInfo(out MethodInfo mi);
                       var attr = mi.DeclaringType.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                       if (attr != null)
                       {
                           return attr.GroupName == docName;
                       }
                       else
                       {
                           return docName == "未分类";
                       }
                   });
                   var ss = options.SwaggerGeneratorOptions;

                   options.IncludeXmlComments("Ron.SwaggerTest.xml");
               });
            }
        }

        static string[] docs = new[] { "未分类", "演示分组" };
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger()
                   .UseSwaggerUI(options =>
                   {
                       options.DocumentTitle = "Ron.liang Swagger 测试文档";
                       foreach (var item in docs)
                           options.SwaggerEndpoint($"/swagger/{item}/swagger.json", item);
                   });
            }
        }
    }
}
