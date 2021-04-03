using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CarTypeService :
        BaseEntityService<IAppUnitOfWork, ICarTypeRepository, CarType, DAL.App.DTO.CarType>,
        ICarTypeService
    {
        public CarTypeService(IAppUnitOfWork serviceUow, ICarTypeRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarTypeMapper(mapper))
        {
        }

        public override async Task<CarType> AddAsync(CarType entity, Guid? userId)
        {
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            return await base.AddAsync(entity, userId);
        }

        public override async Task<CarType> UpdateAsync(CarType entity, Guid? userId)
        {
            entity.UpdatedBy = (Guid) userId!;
            entity.UpdatedAt = DateTime.Now;
            return await base.UpdateAsync(entity, userId);
        }
    }
}