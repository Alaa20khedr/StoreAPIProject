using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.CachServices
{
    public class CacheServices : ICacheServices
    {
        private readonly IDatabase _database;
        public CacheServices(IConnectionMultiplexer redis) {
             _database=redis.GetDatabase();
        }
        public async Task<string> GetCacheResponse(string key)
        {
           var cachedResponse= await _database.StringGetAsync(key);
            if (cachedResponse.IsNullOrEmpty)
                return null;
            return cachedResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object Response, TimeSpan TimeToLive)
        {
            if (Response is null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse=JsonSerializer.Serialize(Response, options);

            await _database.StringSetAsync(key, serializedResponse, TimeToLive);
        }
    }
}
