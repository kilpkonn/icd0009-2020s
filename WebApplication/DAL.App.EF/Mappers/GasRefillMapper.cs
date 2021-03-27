using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class GasRefillMapper : BaseMapper<GasRefill, Domain.App.GasRefill>
    {
        public GasRefillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}