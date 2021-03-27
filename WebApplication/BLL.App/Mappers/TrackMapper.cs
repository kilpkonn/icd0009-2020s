using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class TrackMapper : BaseMapper<Track, DAL.App.DTO.Track>
    {
        public TrackMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}