using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class TrackLocationService : BaseEntityService<IAppUnitOfWork, ITrackLocationRepository,
            TrackLocation, DAL.App.DTO.TrackLocation>, ITrackLocationService
    {
        public TrackLocationService(IAppUnitOfWork serviceUow, ITrackLocationRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new TrackLocationMapper(mapper))
        {
        }
    }
}