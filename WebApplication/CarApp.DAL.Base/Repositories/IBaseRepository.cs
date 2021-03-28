using System;
using Car.DAL.Base.Models;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
        where TEntity : class, IDalEntityId<Guid>
    {
    }

    public interface IBaseRepository<TKey, TEntity> : IBaseRepositoryCommon<TKey, TEntity>,
        IBaseRepositoryAsync<TKey, TEntity>
        where TKey : struct,  IEquatable<TKey>
        where TEntity : class, IDalEntityId<TKey>
    {
    }
}