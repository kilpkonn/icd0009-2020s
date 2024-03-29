using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.DAL.Base;
using Car.DAL.Base.Models;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using CarApp.BLL.Base.Mappers;
using CarApp.BLL.Base.Models;
using CarApp.BLL.Base.Services;
using Microsoft.AspNetCore.Identity;

namespace BLL.Base.Services
{
    public class BaseEntityService<TUnitOfWork, TRepository, TBllEntity, TDalentity>
        : BaseEntityService<Guid, TUnitOfWork, TRepository, TBllEntity, TDalentity>,
            IBaseEntityService<TBllEntity, TDalentity>
        where TBllEntity : class, IBllEntityId<Guid>
        where TDalentity : class, IDalEntityId<Guid>
        where TUnitOfWork : IBaseUnitOfWork
        where TRepository : IBaseRepository<TDalentity>
    {
        public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository,
            IBaseMapper<TBllEntity, TDalentity> mapper)
            : base(serviceUow, serviceRepository, mapper)
        {
        }
    }

    public class BaseEntityService<TKey, TUnitOfWork, TRepository, TBllEntity, TDalEntity> 
        : IBaseEntityService<TKey, TBllEntity, TDalEntity>
        where TBllEntity : class, IBllEntityId<TKey>
        where TDalEntity : class, IDalEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TUnitOfWork : IBaseUnitOfWork
        where TRepository : IBaseRepository<TKey, TDalEntity>
    {
        protected TUnitOfWork ServiceUow;
        protected TRepository ServiceRepository;
        protected IBaseMapper<TBllEntity, TDalEntity> Mapper;
        private readonly Dictionary<TBllEntity, TDalEntity> _entityCache = new();

        public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository,
            IBaseMapper<TBllEntity, TDalEntity> mapper)
        {
            ServiceUow = serviceUow;
            ServiceRepository = serviceRepository;
            Mapper = mapper;
        }

#pragma warning disable 1998
        public virtual async Task<TBllEntity> AddAsync(TBllEntity entity, TKey? userId)
        {
            var dalEntity = Mapper.Map(entity)!;
            var updatedDalEntity = ServiceRepository.Add(dalEntity);
            var returnBllEntity = Mapper.Map(updatedDalEntity)!;
            
            _entityCache.Add(returnBllEntity, dalEntity);
            
            return returnBllEntity;
        }
#pragma warning restore 1998
        
        public TBllEntity GetUpdatedEntityAfterSaveChanges(TBllEntity entity)
        {
            var dalEntity = _entityCache[entity]!;
            var updatedDalEntity = ServiceRepository.GetUpdatedEntityAfterSaveChanges(dalEntity);
            var bllEntity = Mapper.Map(updatedDalEntity)!;
            return bllEntity;
        }

        
        public virtual async Task<TBllEntity> UpdateAsync(TBllEntity entity, TKey? userId)
        {
            TBllEntity dbEntity = Mapper.Map(await ServiceRepository.FirstOrDefaultAsyncNoIncludes(entity.Id, userId))!;

            foreach (var toProp in typeof(TBllEntity).GetProperties())
            {
                var fromProp = typeof(TBllEntity).GetProperty(toProp.Name);
                var toVal = fromProp?.GetValue(entity, null);
                if (toVal != null)
                {
                    toProp.SetValue(dbEntity, toVal, null);
                }
            }
            
            return Mapper.Map(ServiceRepository.Update(Mapper.Map(dbEntity)!, userId))!;
        }

        public virtual TBllEntity Remove(TBllEntity entity, TKey? userId = default)
        {
            return Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)!, userId))!;
        }

        public virtual async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool tracking = false)
        {
            return (await ServiceRepository.GetAllAsync(userId, tracking)).Select(entity => Mapper.Map(entity))!;
        }

        public virtual async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool tracking = false)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId, tracking));
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public virtual async Task<TBllEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            return Mapper.Map(await ServiceRepository.RemoveAsync(id, userId))!;
        }
    }
}