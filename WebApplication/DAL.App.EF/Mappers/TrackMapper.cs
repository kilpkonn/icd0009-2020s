using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class TrackMapper : BaseMapper<Track, Domain.App.Track>
    {
        public TrackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}