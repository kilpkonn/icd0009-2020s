using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class TrackMapper : BaseMapper<Track, BLL.App.DTO.Track>
    {
        public TrackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewTrackMapper : BaseMapper<NewTrack, BLL.App.DTO.Track>
    {
        public NewTrackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}