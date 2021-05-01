using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Models;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepositoryAsync<TKey, TEntity>
        where TKey : struct, IEquatable<TKey>
        where TEntity : class, IDalEntityId<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId, bool tracking = false);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool tracking = false);
        Task<TEntity?> FirstOrDefaultAsyncNoIncludes(TKey id, TKey? userId, bool tracking = false);
        Task<bool> ExistsAsync(TKey id, TKey? userId);
        Task<TEntity> RemoveAsync(TKey id, TKey? userId);
    }
}