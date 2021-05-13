using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class TrackLocationMapper : BaseMapper<TrackLocation, BLL.App.DTO.TrackLocation>
    {
        public TrackLocationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewTrackLocationMapper : BaseMapper<NewTrackLocation, BLL.App.DTO.TrackLocation>
    {
        public NewTrackLocationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}