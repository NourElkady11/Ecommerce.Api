using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class GenaricRepository<TEnity, Tkey> : IGenericRepository<TEnity, Tkey> where TEnity : BaseEntity<Tkey>
    {
        private readonly StoreContext storeContext;

        public GenaricRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        public async Task AddAsync(TEnity entity)
        {
           await storeContext.Set<TEnity>().AddAsync(entity);
        }

        public void DeleteAsync(TEnity entity)
        {
             storeContext.Set<TEnity>().Remove(entity);
        }

        public async Task<IEnumerable<TEnity>> GetAllEelemntsAsync(bool trakChanges = false)
        {
            return trakChanges? await storeContext.Set<TEnity>().ToListAsync(): await storeContext.Set<TEnity>().AsNoTracking().ToListAsync();
           
        }

        public async Task<TEnity> GetElementByID(Tkey id)
        {
            return await storeContext.Set<TEnity>().FindAsync(id);
        }

        public void UpdateAsync(TEnity entity)
        {
             storeContext.Set<TEnity>().Update(entity);
        }
    }
}
