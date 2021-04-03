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
    public class CarMarkService :
        BaseEntityService<IAppUnitOfWork, ICarMarkRepository, CarMark, DAL.App.DTO.CarMark>,
        ICarMarkService
    {
        public CarMarkService(IAppUnitOfWork serviceUow, ICarMarkRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarMarkMapper(mapper))
        {
        }

        public override async Task<CarMark> AddAsync(CarMark entity, Guid? userId)
        {
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            return await base.AddAsync(entity, userId);
        }

        public override async Task<CarMark> UpdateAsync(CarMark entity, Guid? userId)
        {
            entity.UpdatedBy = (Guid) userId!;
            entity.UpdatedAt = DateTime.Now;
            return await base.UpdateAsync(entity, userId);
        }
    }
}