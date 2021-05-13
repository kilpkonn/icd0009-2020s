using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarModelMapper : BaseMapper<CarModel, BLL.App.DTO.CarModel>
    {
        public CarModelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewCarModelMapper : BaseMapper<NewCarModel, BLL.App.DTO.CarModel>
    {
        public NewCarModelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}