using CatalogService.Application.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CatalogService.Infrastructure.Storage
{
    public class CacheService(IDistributedCache cache) : ICacheService
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<T?> GetAsync<T>(string key)
        {
            var objectString = await _cache.GetStringAsync(key) ?? string.Empty;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            return JsonConvert.DeserializeObject<T>(objectString, settings);
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
