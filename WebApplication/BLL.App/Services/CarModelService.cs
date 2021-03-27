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
    }
}