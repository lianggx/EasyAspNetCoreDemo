using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Ron.FilterDemo.Filters
{
    public class CustomerResultFilter : Attribute, IResultFilter
    {
        public CustomerResultFilter()
        {

        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var env = (IHostingEnvironment)context.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("OnResultExecuting,{0}", env.EnvironmentName);
            Console.ForegroundColor = ConsoleColor.Gray;

            // 干预结果
            if (env.IsDevelopment())
                context.HttpContext.Response.Headers.Add("Author", "From Ron.liang");
        }
    }
}
