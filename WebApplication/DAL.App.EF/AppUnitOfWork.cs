using System;
using System.Collections.Generic;
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
        }

        public ICarAccessRepository CarAccesses => GetRepository(() => new CarAccessRepository(UowDbContext));

        public ICarAccessTypeRepository CarAccessTypes => GetRepository(() => new CarAccessTypeRepository(UowDbContext));
        public ICarErrorCodeRepository CarErrorCodes => GetRepository(() => new CarErrorCodeRepository(UowDbContext));
        public ICarMarkRepository CarMarks => GetRepository(() => new CarMarkRepository(UowDbContext));
        public ICarModelRepository CarModels => GetRepository(() => new CarModelRepository(UowDbContext));
        public ICarRepository Cars => GetRepository(() => new CarRepository(UowDbContext));
        public ICarTypeRepository CarTypes => GetRepository(() => new CarTypeRepository(UowDbContext));
        public IGasRefillRepository GasRefills => GetRepository(() => new GasRefillRepository(UowDbContext));
        public ITrackLocationRepository TrackLocations => GetRepository(() => new TrackLocationRepository(UowDbContext));

        public ITrackRepository Tracks => GetRepository(() => new TrackRepository(UowDbContext));
    }
}