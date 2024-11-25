using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICachService
    {
        public Task<string?> GetCacheitem(string cachekey);

        public Task SetCacheitem(string cacheke,object value,TimeSpan duration);
    }
}
