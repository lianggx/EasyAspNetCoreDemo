using System;
using System.Collections;
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

namespace Ron.PhoneTest
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            CreatePhoneValidator(services);
        }
        private void CreatePhoneValidator(IServiceCollection services)
        {
            Hashtable segment = new Hashtable();
            var coll = Configuration.GetSection("phone-segment").GetChildren();
            foreach (var prefix in coll)
            {
                if (string.IsNullOrEmpty(prefix.Value))
                    continue;
                foreach (var s in prefix.Value.Split(','))
                    segment[s] = s;
            }
            var pv = new PhoneValidator(segment);
            services.AddSingleton<PhoneValidator>(pv);
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
