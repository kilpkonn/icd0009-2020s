using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class TrackLocationRepository: BaseRepository<TrackLocation>, ITrackLocationRepository
    {
        public TrackLocationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}