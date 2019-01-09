using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.FilterDemo.Filters
{
    public class CustomerExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("发生了异常：{0}", context.Exception.Message);
            Console.ForegroundColor = ConsoleColor.Gray;

            context.Result = new JsonResult(new { code = 500, message = context.Exception.Message });
            context.ExceptionHandled = true;
        }
    }
}
