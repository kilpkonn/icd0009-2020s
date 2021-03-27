using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarModelMapper : BaseMapper<CarModel, Domain.App.CarModel>
    {
        public CarModelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}