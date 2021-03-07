using System;
using System.Collections.Generic;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepositorySync<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>
    {
        IEnumerable<TEntity> GetAll(bool tracking = false);
        TEntity FirstOrDefault(TKey id, bool tracking = false);
        bool Exists(TKey id);
        TEntity Remove(TKey id);
    }
}