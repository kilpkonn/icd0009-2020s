using AutoMapper;

namespace BLL.App.Mappers
{
    public class CarAccessTypeMapper : BaseMapper<BLL.App.DTO.CarAccessType, DAL.App.DTO.CarAccessType>
    {
        public CarAccessTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}