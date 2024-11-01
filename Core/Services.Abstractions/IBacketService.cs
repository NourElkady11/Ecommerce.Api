using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBacketService
    {
        public Task<BasketDto?> GetBacketAsync(string id);

        public Task<BasketDto?> CreateOrUpdateBacketAsync(BasketDto backetDto);

        public Task<bool> DeleteBacketAsync(string id);

    }
}
