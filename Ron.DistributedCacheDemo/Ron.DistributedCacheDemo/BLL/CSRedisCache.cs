using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.DistributedCacheDemo.BLL
{
    public class CSRedisCache : IDistributedCache, IDisposable
    {
        private CSRedis.CSRedisClient client;
        private CSRedisClientOptions _options;
        public CSRedisCache(IOptions<CSRedisClientOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            if (_options.NodeRule != null && _options.ConnectionStrings != null)
                client = new CSRedis.CSRedisClient(_options.NodeRule, _options.ConnectionStrings);
            else if (_options.ConnectionString != null)
                client = new CSRedis.CSRedisClient(_options.ConnectionString);
            else
                throw new ArgumentNullException(nameof(_options.ConnectionString));

            RedisHelper.Initialization(client);
        }
        public void Dispose()
        {
            if (client != null)
                client.Dispose();
        }

        public byte[] Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return RedisHelper.Get<byte[]>(key);
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            token.ThrowIfCancellationRequested();

            return await RedisHelper.GetAsync<byte[]>(key);
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            RedisHelper.Del(key);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await RedisHelper.DelAsync(key);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            RedisHelper.Set(key, value);
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await RedisHelper.SetAsync(key, value);
        }
    }
}
