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
    }
}