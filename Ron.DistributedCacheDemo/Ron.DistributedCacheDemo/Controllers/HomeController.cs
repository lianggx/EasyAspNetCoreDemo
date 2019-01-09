using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Ron.DistributedCacheDemo.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private IDistributedCache cache;
        public HomeController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [HttpGet("Index")]
        public async Task<ActionResult<string>> SetTime()
        {
            var CurrentTime = DateTime.Now.ToString();
            await this.cache.SetStringAsync("CurrentTime", CurrentTime);
            return CurrentTime;
        }

        [HttpGet("GetTime")]
        public async Task<ActionResult<string>> GetTime()
        {
            var CurrentTime = await this.cache.GetStringAsync("CurrentTime");
            return CurrentTime;
        }
    }
}
