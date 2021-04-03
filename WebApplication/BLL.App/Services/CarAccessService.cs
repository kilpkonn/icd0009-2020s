using System;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CarAccessService : BaseEntityService<IAppUnitOfWork, ICarAccessRepository, CarAccess,
            DAL.App.DTO.CarAccess>, ICarAccessService
    {
        public CarAccessService(IAppUnitOfWork serviceUow, ICarAccessRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarAccessMapper(mapper))
        {
            
            
        }

        public override CarAccess Add(CarAccess entity, Guid? userId)
        {
            entity.AppUserId = (Guid) userId!;
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            return base.Add(entity, userId);
        }

        public override CarAccess Update(CarAccess entity, Guid? userId)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = (Guid) userId!;
            return base.Update(entity, userId);
        }
    }
}