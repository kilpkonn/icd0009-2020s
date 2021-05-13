using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarMarkMapper : BaseMapper<CarMark, BLL.App.DTO.CarMark>
    {
        public CarMarkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
    
    public class NewCarMarkMapper : BaseMapper<NewCarMark, BLL.App.DTO.CarMark>
    {
        public NewCarMarkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}