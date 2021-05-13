using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarAccessTypeMapper : BaseMapper<CarAccessType, BLL.App.DTO.CarAccessType>
    {
        public CarAccessTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewCarAccessTypeMapper : BaseMapper<NewCarAccessType, BLL.App.DTO.CarAccessType>
    {
        public NewCarAccessTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}