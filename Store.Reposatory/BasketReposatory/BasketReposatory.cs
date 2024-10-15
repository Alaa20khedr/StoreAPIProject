using StackExchange.Redis;
using Store.Reposatory.BasketReposatory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Store.Reposatory.BasketReposatory
{
    public class BasketReposatory : IBasketReposatory
    {
        private readonly IDatabase _database;
        public BasketReposatory(IConnectionMultiplexer redis) { 
            _database=redis.GetDatabase();  

        }
        public async Task<bool> DeleteBasketAsync(string BasketId) 
       =>await _database.KeyDeleteAsync(BasketId);

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var Data = await _database.StringGetAsync(BasketId);
            return Data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var IsCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!IsCreated)
                return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
