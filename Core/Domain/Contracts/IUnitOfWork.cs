using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();

        IGenericRepository<Entity,TKey> GetRepository<Entity,TKey>()where Entity:BaseEntity<TKey> ;
    }
}
