using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class TrackLocationMapper : BaseMapper<TrackLocation, DAL.App.DTO.TrackLocation>
    {
        public TrackLocationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}