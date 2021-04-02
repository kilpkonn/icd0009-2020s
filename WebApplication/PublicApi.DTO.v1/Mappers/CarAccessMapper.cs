using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarAccessMapper : BaseMapper<CarAccess, BLL.App.DTO.CarAccess>
    {
        public CarAccessMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}