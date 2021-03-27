using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class CarMapper : BaseMapper<DTO.Car, Domain.App.Car>
    {
        public CarMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}