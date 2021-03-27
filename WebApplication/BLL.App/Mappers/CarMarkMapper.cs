using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CarMarkMapper : BaseMapper<CarMark, DAL.App.DTO.CarMark>
    {
        public CarMarkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}