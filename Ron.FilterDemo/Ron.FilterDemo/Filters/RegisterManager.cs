using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.FilterDemo.Filters
{
    public class RegisterManagerPipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            CookieAuthenticationOptions options = new CookieAuthenticationOptions();

            applicationBuilder.UseCors(config =>
            {
                config.AllowAnyOrigin();
            });
        }
    }
}
