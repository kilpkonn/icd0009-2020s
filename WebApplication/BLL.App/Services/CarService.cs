using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CarService : BaseEntityService<IAppUnitOfWork, ICarRepository, DTO.Car, DAL.App.DTO.Car>,
        ICarService
    {
        public CarService(IAppUnitOfWork serviceUow, ICarRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarMapper(mapper))
        {
        }

        public async Task<IEnumerable<DTO.Car>> GetAccessibleCarsForUser(Guid userId)
        {
            return (await ServiceRepository.GetAccessibleCarsForUser(userId)).Select(e => Mapper.Map(e)!)!;
        }

        public override async Task<DTO.Car> AddAsync(DTO.Car entity, Guid? userId)
        {
            entity.AppUserId = (Guid) userId!;
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            var car = await base.AddAsync(entity, userId);

            var carAccess = new CarAccess()
            {
                Car = Mapper.Map(entity),
                AppUserId = (Guid) userId!,
                CarAccessType = await ServiceUow.CarAccessTypes.FindByNameAsync("Owner")
            };
            ServiceUow.CarAccesses.Add(carAccess);

            return car;
        }

        public override async Task<DTO.Car> UpdateAsync(DTO.Car entity, Guid? userId)
        {
            entity.UpdatedBy = (Guid) userId!;
            entity.UpdatedAt = DateTime.Now;
            return await base.UpdateAsync(entity, userId);
        }
    }
}