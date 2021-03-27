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
    }
}