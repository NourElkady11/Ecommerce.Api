using Domain.Contracts;
using Domain.Entities;
using Persistance.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly StoreContext storeContext;
        private readonly ConcurrentDictionary<string, object> repositories = new ConcurrentDictionary<string, object>();

        public UnitOfWork(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            this.repositories = new ConcurrentDictionary<string, object>();
        }

        public IGenericRepository<Entity, TKey> GetRepository<Entity, TKey>() where Entity : BaseEntity<TKey>
        {
            return (IGenericRepository<Entity, TKey>) repositories.GetOrAdd(typeof(Entity).Name, _ => new GenaricRepository<Entity, TKey>(storeContext));
          

        }

        public async Task<int> SaveChangesAsync()=> await storeContext.SaveChangesAsync();
        
    }
}
