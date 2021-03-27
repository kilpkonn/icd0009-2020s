using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.DAL.Base;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using CarApp.BLL.Base.Mappers;
using CarApp.BLL.Base.Services;

namespace BLL.Base.Services
{
    public class BaseEntityService<TUnitOfWork, TRepository, TBllEntity, TDalentity>
        : BaseEntityService<Guid, TUnitOfWork, TRepository, TBllEntity, TDalentity>,
            IBaseEntityService<TBllEntity, TDalentity>
        where TBllEntity : class, IDomainEntityId<Guid>
        where TDalentity : class, IDomainEntityId<Guid>
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
        where TBllEntity : class, IDomainEntityId<TKey>
        where TDalEntity : class, IDomainEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TUnitOfWork : IBaseUnitOfWork
        where TRepository : IBaseRepository<TKey, TDalEntity>
    {
        protected TUnitOfWork ServiceUow;
        protected TRepository ServiceRepository;
        protected IBaseMapper<TBllEntity, TDalEntity> Mapper;

        public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository,
            IBaseMapper<TBllEntity, TDalEntity> mapper)
        {
            ServiceUow = serviceUow;
            ServiceRepository = serviceRepository;
            Mapper = mapper;
        }

        public TBllEntity Add(TBllEntity entity)
        {
            return Mapper.Map(ServiceRepository.Add(Mapper.Map(entity)!))!;
        }

        public TBllEntity Update(TBllEntity entity, TKey? userId)
        {
            return Mapper.Map(ServiceRepository.Update(Mapper.Map(entity)!, userId))!;
        }

        public TBllEntity Remove(TBllEntity entity, TKey? userId = default)
        {
            return Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)!, userId))!;
        }

        public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool tracking = false)
        {
            return (await ServiceRepository.GetAllAsync(userId, tracking)).Select(entity => Mapper.Map(entity))!;
        }

        public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool tracking = false)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId, tracking));
        }

        public async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task<TBllEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            return Mapper.Map(await ServiceRepository.RemoveAsync(id, userId))!;
        }
    }
}