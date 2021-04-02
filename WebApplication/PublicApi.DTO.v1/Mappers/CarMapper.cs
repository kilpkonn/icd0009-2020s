using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarMapper : BaseMapper<Car, BLL.App.DTO.Car>
    {
        public CarMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}