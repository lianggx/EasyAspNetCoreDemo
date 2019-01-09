using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Ron.MemoryCacheDemo.Models;

namespace Ron.MemoryCacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IMemoryCache cache;
        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            MemoryCacheEntryOptions entry = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            };
            cache.Set("userId", "0001", entry);

            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return cache.Get<string>("userId");
        }
    }
}
