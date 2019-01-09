using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ron.HttpClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private WeatherService weatherService;
        public ValuesController(WeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string result = string.Empty;
            try
            {
                result = await weatherService.GetData();
            }
            catch { }

            return new JsonResult(new { result });
        }
    }
}

public class WeatherService
{
    private HttpClient httpClient;
    public WeatherService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = new Uri("http://www.weather.com.cn");
        this.httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<string> GetData()
    {
        var data = await this.httpClient.GetAsync("/data/sk/101010100.html");
        var result = await data.Content.ReadAsStringAsync();

        return result;
    }
}
