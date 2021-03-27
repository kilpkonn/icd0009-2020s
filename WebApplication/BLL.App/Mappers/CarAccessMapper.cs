using AutoMapper;

namespace BLL.App.Mappers
{
    public class CarAccessMapper : BaseMapper<BLL.App.DTO.CarAccess, DAL.App.DTO.CarAccess>
    {
        public CarAccessMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}