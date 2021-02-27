using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>
    {
    }

    public interface IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
        Task<TEntity> FirstOrDefaultAsync(TKey id, bool tracking = false);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<TEntity> Remove(TKey id);
        Task<bool> ExistsAsync(TKey id);
    }
}