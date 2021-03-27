using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TrackLocationRepository : BaseRepository<DTO.TrackLocation, Domain.App.TrackLocation, AppDbContext>, ITrackLocationRepository
    {
        public TrackLocationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TrackLocationMapper(mapper))
        {
            
        }

        public override async Task<IEnumerable<TrackLocation>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateLocalQuery(userId, tracking).Select(e => Mapper.Map(e)!).ToListAsync();
        }

        public override async Task<TrackLocation?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            return Mapper.Map(await CreateLocalQuery(userId, tracking)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync());
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
        
        protected IQueryable<Domain.App.TrackLocation> CreateLocalQuery(Guid? userId, bool tracking = false)
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