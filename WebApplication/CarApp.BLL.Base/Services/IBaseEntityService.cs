using System;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;

namespace CarApp.BLL.Base.Services
{
    public interface IBaseEntityService<TBllEntity, TDalEntity> : IBaseEntityService<Guid, TBllEntity, TDalEntity>
        where TBllEntity : class, IDomainEntityId<Guid>
        where TDalEntity : class, IDomainEntityId<Guid>
    {
    }

    public interface IBaseEntityService<TKey, TBllEntity, TDalEntity> : IBaseService, IBaseRepository<TKey, TBllEntity>
        where TBllEntity : class, IDomainEntityId<TKey>
        where TDalEntity : class, IDomainEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
    {
    }
}