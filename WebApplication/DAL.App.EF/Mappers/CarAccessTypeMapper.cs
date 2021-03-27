using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarAccessTypeMapper : BaseMapper<CarAccessType, Domain.App.CarAccessType>
    {
        public CarAccessTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}