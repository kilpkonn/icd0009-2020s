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
    public class CarErrorCodeServices : BaseEntityService<IAppUnitOfWork, ICarErrorCodeRepository,
            CarErrorCode, DAL.App.DTO.CarErrorCode>, ICarErrorCodeService
    {
        public CarErrorCodeServices(IAppUnitOfWork serviceUow, ICarErrorCodeRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarErrorCodeMapper(mapper))
        {
        }

        public override async Task<CarErrorCode> AddAsync(CarErrorCode entity, Guid? userId)
        {
            entity.CreatedBy = (Guid) userId!;
            entity.UpdatedBy = (Guid) userId!;
            return await base.AddAsync(entity, userId);
        }
    }
}