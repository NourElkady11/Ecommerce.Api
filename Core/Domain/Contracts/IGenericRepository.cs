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
        Task<TEntity> GetElementByID(Tkey id);

        Task<IEnumerable<TEntity>> GetAllEelemntsAsync(bool trakChanges=false);

        Task AddAsync(TEntity entity);

        void DeleteAsync(TEntity entity);

        void UpdateAsync(TEntity entity);



    }
}
