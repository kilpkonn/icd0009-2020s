using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarTypeMapper : BaseMapper<CarType, BLL.App.DTO.CarType>
    {
        public CarTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewCarTypeMapper : BaseMapper<NewCarType, BLL.App.DTO.CarType>
    {
        public NewCarTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}