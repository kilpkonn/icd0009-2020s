using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class TrackLocationMapper : BaseMapper<TrackLocation, BLL.App.DTO.TrackLocation>
    {
        public TrackLocationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}