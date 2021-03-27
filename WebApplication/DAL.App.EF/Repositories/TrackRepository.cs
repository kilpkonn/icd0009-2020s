using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TrackRepository : BaseRepository<DTO.Track, Domain.App.Track, AppDbContext>, ITrackRepository
    {
        public TrackRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TrackMapper(mapper))
        {
        }
    }
}