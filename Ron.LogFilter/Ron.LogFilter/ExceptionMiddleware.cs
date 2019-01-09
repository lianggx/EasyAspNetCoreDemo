using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;
    private IHostingEnvironment environment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment environment)
    {
        this.next = next;
        this.logger = logger;
        this.environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
            var features = context.Features;
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception e)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/json;charset=utf-8;";
        string error = "";

        void ReadException(Exception ex)
        {
            error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
            if (ex.InnerException != null)
            {
                ReadException(ex.InnerException);
            }
        }

        ReadException(e);
        logger.LogError(error);

        if (environment.IsDevelopment())
        {
            var json = new { message = e.Message, detail = error };
            error = JsonConvert.SerializeObject(json);
        }
        else
            error = "抱歉，出错了";

        await context.Response.WriteAsync(error);
    }
}
