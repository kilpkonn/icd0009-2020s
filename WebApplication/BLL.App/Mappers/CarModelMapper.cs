using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CarModelMapper : BaseMapper<CarModel, DAL.App.DTO.CarModel>
    {
        public CarModelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}