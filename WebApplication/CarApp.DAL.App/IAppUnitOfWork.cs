using Car.DAL.Base;
using CarApp.DAL.App.Repositories;

namespace CarApp.DAL.App
{
    public interface IAppUnitOfWork: IBaseUnitOfWork
    {
        ICarAccessRepository CarAccesses { get; }
        ICarAccessTypeRepository CarAccessTypes { get; }
        ICarErrorCodeRepository CarErrorCodes { get; }
        ICarMarkRepository CarMarks { get; }
        ICarModelRepository CarModels { get; }
        ICarRepository Cars { get; }
        ICarTypeRepository CarTypes { get; }
        IGasRefillRepository GasRefills { get; }
        ITrackLocationRepository TrackLocations { get; }
        ITrackRepository Tracks { get; }
        IUserRepository Users { get; }
    }
}