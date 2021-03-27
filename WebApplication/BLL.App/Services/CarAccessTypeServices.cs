using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CarAccessTypeServices : BaseEntityService<IAppUnitOfWork, ICarAccessTypeRepository, CarAccessType,
            DAL.App.DTO.CarAccessType>, ICarAccessTypeService
    {
        public CarAccessTypeServices(IAppUnitOfWork serviceUow, ICarAccessTypeRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new CarAccessTypeMapper(mapper))
        {
        }
    }
}