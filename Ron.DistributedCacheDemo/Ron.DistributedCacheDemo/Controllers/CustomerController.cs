using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.DistributedCacheDemo.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private IDistributedCache cache;
        public CustomerController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [HttpGet("NewId")]
        public async Task<ActionResult<string>> NewId()
        {
            var id = Guid.NewGuid().ToString("N");
            await this.cache.SetStringAsync("CustomerId", id);
            return id;
        }

        [HttpGet("GetId")]
        public async Task<ActionResult<string>> GetId()
        {
            var id = await this.cache.GetStringAsync("CustomerId");
            return id;
        }
    }
}
