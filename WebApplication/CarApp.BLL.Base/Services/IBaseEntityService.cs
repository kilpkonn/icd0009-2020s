using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Models;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using CarApp.BLL.Base.Models;

namespace CarApp.BLL.Base.Services
{
    public interface IBaseEntityService<TBllEntity, TDalEntity> : IBaseEntityService<Guid, TBllEntity, TDalEntity>
        where TBllEntity : class, IBllEntityId<Guid>
        where TDalEntity : class, IDalEntityId<Guid>
    {
    }

    public interface IBaseEntityService<TKey, TBllEntity, TDalEntity> : IBaseService  // , IBaseRepository<TKey, TBllEntity>
        where TBllEntity : class, IBllEntityId<TKey>
        where TDalEntity : class, IDalEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        TBllEntity Add(TBllEntity entity, TKey? userId);
        TBllEntity Update(TBllEntity entity, TKey? userId);
        TBllEntity Remove(TBllEntity entity, TKey? userId);
        Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId, bool tracking = false);
        Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool tracking = false);
        Task<bool> ExistsAsync(TKey id, TKey? userId);
        Task<TBllEntity> RemoveAsync(TKey id, TKey? userId);
    }
}