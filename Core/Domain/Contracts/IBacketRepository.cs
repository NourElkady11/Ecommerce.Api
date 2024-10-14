using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBacketRepository
    {
        public Task<CustomerBacket> GetcustomerBacketAsync(string id);

        public Task<CustomerBacket> CreateOrUpdateBacketAsync(CustomerBacket backet, TimeSpan? timeToLive=null);

        public Task<bool> DeleteBacketAsync(string id);
    }
}
