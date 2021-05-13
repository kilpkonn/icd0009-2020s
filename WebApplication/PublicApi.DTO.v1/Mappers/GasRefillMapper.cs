using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GasRefillMapper : BaseMapper<GasRefill, BLL.App.DTO.GasRefill>
    {
        public GasRefillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewGasRefillMapper : BaseMapper<NewGasRefill, BLL.App.DTO.GasRefill>
    {
        public NewGasRefillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}