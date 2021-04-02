using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarErrorCodeMapper : BaseMapper<CarErrorCode, BLL.App.DTO.CarErrorCode>
    {
        public CarErrorCodeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}