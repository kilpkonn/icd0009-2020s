using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarMarkMapper : BaseMapper<CarMark, Domain.App.CarMark>
    {
        public CarMarkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}