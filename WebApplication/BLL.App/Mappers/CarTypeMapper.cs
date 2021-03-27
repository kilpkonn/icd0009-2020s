using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CarTypeMapper : BaseMapper<CarType, DAL.App.DTO.CarType>
    {
        public CarTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}