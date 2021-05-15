using AutoMapper;
using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF;
using DAL.App.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public ICarAccessRepository CarAccesses => GetRepository(() => new CarAccessRepository(UowDbContext, Mapper));
        public ICarAccessTypeRepository CarAccessTypes => GetRepository(() => new CarAccessTypeRepository(UowDbContext, Mapper));
        public ICarErrorCodeRepository CarErrorCodes => GetRepository(() => new CarErrorCodeRepository(UowDbContext, Mapper));
        public ICarMarkRepository CarMarks => GetRepository(() => new CarMarkRepository(UowDbContext, Mapper));
        public ICarModelRepository CarModels => GetRepository(() => new CarModelRepository(UowDbContext, Mapper));
        public ICarRepository Cars => GetRepository(() => new CarRepository(UowDbContext, Mapper));
        public ICarTypeRepository CarTypes => GetRepository(() => new CarTypeRepository(UowDbContext, Mapper));
        public IGasRefillRepository GasRefills => GetRepository(() => new GasRefillRepository(UowDbContext, Mapper));
        public ITrackLocationRepository TrackLocations => GetRepository(() => new TrackLocationRepository(UowDbContext, Mapper));
        public ITrackRepository Tracks => GetRepository(() => new TrackRepository(UowDbContext, Mapper));
        public IUserRepository Users => GetRepository(() => new UserRepository(UowDbContext, Mapper));
    }
}