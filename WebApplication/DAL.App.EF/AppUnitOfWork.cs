using CarApp.DAL.App;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF;
using DAL.EF.Repositories;

namespace DAL.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
            CarAccesses = new CarAccessRepository(uowDbContext);
            CarAccessTypes = new CarAccessTypeRepository(uowDbContext);
            CarErrorCodes = new CarErrorCodeRepository(uowDbContext);
            CarMarks = new CarMarkRepository(uowDbContext);
            CarModels = new CarModelRepository(uowDbContext);
            Cars = new CarRepository(uowDbContext);
            CarTypes = new CarTypeRepository(uowDbContext);
            GasRefills = new GasRefillRepository(uowDbContext);
            TrackLocations = new TrackLocationRepository(uowDbContext);
            Tracks = new TrackRepository(uowDbContext);
        }

        public ICarAccessRepository CarAccesses { get; }
        public ICarAccessTypeRepository CarAccessTypes { get; }
        public ICarErrorCodeRepository CarErrorCodes { get; }
        public ICarMarkRepository CarMarks { get; }
        public ICarModelRepository CarModels { get; }
        public ICarRepository Cars { get; }
        public ICarTypeRepository CarTypes { get; }
        public IGasRefillRepository GasRefills { get; }
        public ITrackLocationRepository TrackLocations { get; }
        public ITrackRepository Tracks { get; }
    }
}