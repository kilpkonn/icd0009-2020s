using System;
using System.Collections.Generic;
using Car.DAL.Base.Models;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepositorySync<TKey, out TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDalEntityId<TKey>
    {
        IEnumerable<TEntity> GetAll(bool tracking = false);
        TEntity FirstOrDefault(TKey id, bool tracking = false);
        bool Exists(TKey id);
        TEntity Remove(TKey id);
    }
}