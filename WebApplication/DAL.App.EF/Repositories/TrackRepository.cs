using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class TrackRepository: BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}