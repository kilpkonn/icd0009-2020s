using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TrackLocationRepository : BaseRepository<TrackLocation, AppDbContext>, ITrackLocationRepository
    {
        public TrackLocationRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<IEnumerable<TrackLocation>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateLocalQuery(userId, tracking).ToListAsync();
        }

        public override async Task<TrackLocation?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            return await CreateLocalQuery(userId, tracking)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public override TrackLocation Update(TrackLocation entity, Guid? userId)
        {
            // TODO: This broken atm
            return base.Update(entity, userId);
        }

        public override TrackLocation Remove(TrackLocation entity, Guid? userId)
        {
            // TODO: This broken atm
            return base.Remove(entity, userId);
        }

        public override async Task<TrackLocation> RemoveAsync(Guid id, Guid? userId)
        {
            // TODO: This broken atm
            return await base.RemoveAsync(id, userId);
        }

        public override async Task<bool> ExistsAsync(Guid id, Guid? userId)
        {
            return await CreateLocalQuery(id).AnyAsync(l => l.Id == id);
        }
        
        protected IQueryable<TrackLocation> CreateLocalQuery(Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(null, tracking);

            if (userId != null)
            {
                query = query.Include(l => l.Track)
                    .Where(l => l.Track!.AppUserId == userId);
            }
            return query;
        }
    }
}