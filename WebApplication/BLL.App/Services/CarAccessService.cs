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
    }
}