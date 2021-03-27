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
    }
}