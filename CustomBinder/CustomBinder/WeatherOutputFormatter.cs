using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomBinder
{
    public class WeatherOutputFormatter : TextOutputFormatter
    {
        private readonly static Type WeatherForecastType = typeof(WeatherForecast);
        public WeatherOutputFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add("text/weather");
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.ObjectType == WeatherForecastType || context.Object is WeatherForecast || context.ObjectType.GenericTypeArguments[0] == WeatherForecastType)
            {
                return base.CanWriteResult(context);
            }

            return false;
        }


        private string WriterText(IEnumerable<WeatherForecast> weathers)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var wealther in weathers)
            {
                builder.Append(WriterText(wealther));
            }
            return builder.ToString();
        }

        private string WriterText(WeatherForecast weather) => $"date={WebUtility.UrlEncode(weather.Date.ToString())}&temperatureC={weather.TemperatureC}&temperatureF={weather.TemperatureF}&summary={WebUtility.UrlEncode(weather.Summary)}";

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (selectedEncoding == null)
            {
                throw new ArgumentNullException(nameof(selectedEncoding));
            }

            string text = string.Empty;

            if (context.ObjectType == WeatherForecastType)
                text = WriterText(context.Object as WeatherForecast);
            else if (context.ObjectType.GenericTypeArguments[0] == WeatherForecastType)
                text = WriterText(context.Object as IEnumerable<WeatherForecast>);

            if (string.IsNullOrEmpty(text))
            {
                await Task.CompletedTask;
            }

            var response = context.HttpContext.Response;
            await response.WriteAsync(text, selectedEncoding);
        }
    }
}
