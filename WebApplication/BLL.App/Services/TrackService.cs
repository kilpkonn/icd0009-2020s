using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class TrackService :
        BaseEntityService<IAppUnitOfWork, ITrackRepository, Track, DAL.App.DTO.Track>,
        ITrackService
    {
        public TrackService(IAppUnitOfWork serviceUow, ITrackRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new TrackMapper(mapper))
        {
        }
    }
}