using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
       

        Task<TEntity?> GetAsync(Tkey id);

        Task<TEntity?> GetAsync(Specifications<TEntity> specifications);

        Task<int> CountAsync(Specifications<TEntity?> specifications);

        Task<IEnumerable<TEntity>?> GetAllAsync(bool trakChanges=false); 

        Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);

        Task AddAsync(TEntity entity);

        void DeleteAsync(TEntity entity);

        void UpdateAsync(TEntity entity);



    }
}
