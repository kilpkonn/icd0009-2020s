using AutoMapper;

namespace BLL.App.Mappers
{
    public class CarMapper : BaseMapper<BLL.App.DTO.Car, DAL.App.DTO.Car>
    {
        public CarMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}