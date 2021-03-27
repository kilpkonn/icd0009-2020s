using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarTypeMapper : BaseMapper<CarType, Domain.App.CarType>
    {
        public CarTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}