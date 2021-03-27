using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class TrackLocationMapper : BaseMapper<TrackLocation, Domain.App.TrackLocation>
    {
        public TrackLocationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}