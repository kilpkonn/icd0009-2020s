using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CarService : BaseEntityService<IAppUnitOfWork, ICarRepository, DTO.Car, DAL.App.DTO.Car>,
        ICarService
    {
        public CarService(IAppUnitOfWork serviceUow, ICarRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarMapper(mapper))
        {
        }
    }
}