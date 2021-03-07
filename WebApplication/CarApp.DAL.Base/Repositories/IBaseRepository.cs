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

    public interface IBaseRepository<TKey, TEntity> : IBaseRepositoryCommon<TKey, TEntity>,
        IBaseRepositoryAsync<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>
    {
    }
}