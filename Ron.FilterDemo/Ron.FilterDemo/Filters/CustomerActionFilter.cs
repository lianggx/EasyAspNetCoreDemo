using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.FilterDemo.Filters
{
    public class CustomerActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("==== OnActionExecuting");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("==== OnActionExecuted");
            base.OnActionExecuted(context);
        }
    }

    public class UserNameActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==== UserName ActionFilter，Order：{0}", this.Order);
            Console.ForegroundColor = ConsoleColor.Gray;
            base.OnActionExecuting(context);
        }
    }

    public class UserAgeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==== UserAge ActionFilter，Order：{0}", this.Order);
            Console.ForegroundColor = ConsoleColor.Gray;
            base.OnActionExecuting(context);
        }
    }
}
