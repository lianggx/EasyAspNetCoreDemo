using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.DistributedCacheDemo.BLL
{
    public static class CSRedisCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddCSRedisCache(this IServiceCollection services, Action<CSRedisClientOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, CSRedisCache>());

            return services;
        }
    }
}
