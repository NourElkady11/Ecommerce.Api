using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : IChacheRepository
    {
        private readonly IDatabase redisdatabase=connectionMultiplexer.GetDatabase();

        public async Task<string?> GetCacheitemAsync(string key)
        {
           var value=await redisdatabase.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value;
            //defult of the return type that is Task<string?> here 
        }

        public async Task SetCacheitemAsync(string key, object value, TimeSpan Duration)
        {
            var JsonObj=JsonSerializer.Serialize(value);
            await redisdatabase.StringSetAsync(key,JsonObj,Duration);
        }
    }
}