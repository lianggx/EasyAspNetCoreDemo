using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.FilterDemo.Filters
{
    public class CustomerResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("==== OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==== OnResourceExecuting");
            Console.ForegroundColor = ConsoleColor.Gray;          
        }
    }
}
