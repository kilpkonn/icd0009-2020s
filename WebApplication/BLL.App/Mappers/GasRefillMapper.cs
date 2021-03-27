using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class GasRefillMapper : BaseMapper<GasRefill, DAL.App.DTO.GasRefill>
    {
        public GasRefillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}