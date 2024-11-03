using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CatalogService.Infrastructure.CacheStorage
{
    public class CacheService(IDistributedCache cache) : ICacheService
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<T?> GetAsync<T>(string key)
        {
            var objectString = await _cache.GetStringAsync(key) ?? string.Empty;

            return JsonConvert.DeserializeObject<T>(objectString);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200),
            };

            var objectString = JsonConvert.SerializeObject(data);
            await _cache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
        }
    }
}
