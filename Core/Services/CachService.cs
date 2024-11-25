using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CachService(IChacheRepository chacheRepository) : ICachService
    {
        public async Task<string?> GetCacheitem(string cachekey)=>await chacheRepository.GetCacheitemAsync(cachekey);
       

        public async Task SetCacheitem(string cacheke, object value, TimeSpan duration)=>await chacheRepository.SetCacheitemAsync(cacheke, value, duration);


    }
}
