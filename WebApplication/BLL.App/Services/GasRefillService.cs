using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class GasRefillService : BaseEntityService<IAppUnitOfWork, IGasRefillRepository, GasRefill,
            DAL.App.DTO.GasRefill>, IGasRefillService
    {
        public GasRefillService(IAppUnitOfWork serviceUow, IGasRefillRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new GasRefillMapper(mapper))
        {
        }
    }
}