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
    public class TokenController : ControllerBase
    {
        private IMemoryCache cache;
        public TokenController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet("login")]
        public ActionResult<string> Login()
        {
            var cts = new CancellationTokenSource();
            cache.Set(CacheKeys.DependentCTS, cts);
            using (var entry = cache.CreateEntry(CacheKeys.UserSession))
            {
                entry.Value = "_x0123456789";
                entry.RegisterPostEvictionCallback(DependentEvictionCallback, this);
                cache.Set(CacheKeys.UserShareData, "这里是共享的数据", new CancellationChangeToken(cts.Token));
                cache.Set(CacheKeys.UserCart, "这里是购物车", new CancellationChangeToken(cts.Token));
            }
            return "设置依赖完成";
        }

        [HttpPost("getkeys")]
        public IActionResult GetKeys()
        {
            var userInfo = new
            {
                UserSession = cache.Get<string>(CacheKeys.UserSession),
                UserShareData = cache.Get<string>(CacheKeys.UserShareData),
                UserCart = cache.Get<string>(CacheKeys.UserCart)
            };

            return new JsonResult(userInfo);
        }

        [HttpPost("logout")]
        public ActionResult<string> LogOut()
        {
            cache.Get<CancellationTokenSource>(CacheKeys.DependentCTS).Cancel();

            var userInfo = new
            {
                UserSession = cache.Get<string>(CacheKeys.UserSession),
                UserShareData = cache.Get<string>(CacheKeys.UserShareData),
                UserCart = cache.Get<string>(CacheKeys.UserCart)
            };

            return new JsonResult(userInfo);
        }

        private static void DependentEvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Key:{0} 已过期，依赖于该 Key 的所有缓存都将过期而处于不可用状态", key);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
