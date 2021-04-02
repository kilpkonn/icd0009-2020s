using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarModelMapper : BaseMapper<CarModel, BLL.App.DTO.CarModel>
    {
        public CarModelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}