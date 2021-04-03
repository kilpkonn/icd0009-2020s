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
    public class CarModelService : BaseEntityService<IAppUnitOfWork, ICarModelRepository, CarModel,
            DAL.App.DTO.CarModel>, ICarModelService
    {
        public CarModelService(IAppUnitOfWork serviceUow, ICarModelRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarModelMapper(mapper))
        {
        }

        public override async Task<CarModel> AddAsync(CarModel entity, Guid? userId)
        {
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            return await base.AddAsync(entity, userId);
        }

        public override async Task<CarModel> UpdateAsync(CarModel entity, Guid? userId)
        {
            entity.UpdatedBy = (Guid) userId!;
            entity.UpdatedAt = DateTime.Now;
            return await base.UpdateAsync(entity, userId);
        }
    }
}