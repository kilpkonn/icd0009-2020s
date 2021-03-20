using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TrackRepository : BaseRepository<Track, AppDbContext>, ITrackRepository
    {
        public TrackRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}