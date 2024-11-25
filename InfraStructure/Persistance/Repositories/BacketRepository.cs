using Domain.Contracts;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BacketRepository(IConnectionMultiplexer connectionMultiplexer) : IBacketRepository
    {
        private readonly IDatabase dataBase=connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBacketAsync(string id)=> await dataBase.KeyDeleteAsync(id);

        public async Task<CustomerBacket> GetcustomerBacketAsync(string id)
        {
            var value=await dataBase.StringGetAsync(id); //return json varible of type of redis value
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBacket>(value);
            }

        }

        public async Task<CustomerBacket?> CreateOrUpdateBacketAsync(CustomerBacket backet, TimeSpan? timeToLive = null)
        {
            var jsonbacket=JsonSerializer.Serialize(backet);

            // it will create or update backett
            var iscreatedOrUpdated = await dataBase.StringSetAsync(backet.Id, jsonbacket, timeToLive??TimeSpan.FromDays(1));

            return iscreatedOrUpdated ? await GetcustomerBacketAsync(backet.Id) : null;
        }
    }
}
