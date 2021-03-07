using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.EF.Repositories
{
    public class TrackLocationRepository : BaseRepository<TrackLocation, AppDbContext>, ITrackLocationRepository
    {
        public TrackLocationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}