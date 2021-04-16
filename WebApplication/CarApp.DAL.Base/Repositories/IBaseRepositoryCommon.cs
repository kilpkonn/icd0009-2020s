using System;
using Car.DAL.Base.Models;
using Car.Domain.Base;

namespace Car.DAL.Base.Repositories
{
    public interface IBaseRepositoryCommon<TKey, TEntity>
        where TKey : struct, IEquatable<TKey>
        where TEntity : class, IDalEntityId<TKey>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity, TKey? userId);
        TEntity Remove(TEntity entity, TKey? userId);
        TEntity GetUpdatedEntityAfterSaveChanges(TEntity entity);
    }
}