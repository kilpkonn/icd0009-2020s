using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepositoryAsync<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, bool tracking = false);
        Task<bool> ExistsAsync(TKey id);
        Task<TEntity> RemoveAsync(TKey id);
    }
}