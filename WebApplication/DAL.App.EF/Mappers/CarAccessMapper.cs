using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarAccessMapper : BaseMapper<CarAccess, Domain.App.CarAccess>
    {
        public CarAccessMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}