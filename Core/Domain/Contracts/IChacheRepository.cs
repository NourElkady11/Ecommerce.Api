using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IChacheRepository
    {
        public Task SetCacheitemAsync(string key, object value,TimeSpan Duration);

        public Task<string?> GetCacheitemAsync(string key);
    }
}
