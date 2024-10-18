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
        public Task<BacketDto?> GetBacketAsync(string id);

        public Task<BacketDto?> CreateOrUpdateBacketAsync(BacketDto backetDto);

        public Task<bool> DeleteBacketAsync(string id);

    }
}
