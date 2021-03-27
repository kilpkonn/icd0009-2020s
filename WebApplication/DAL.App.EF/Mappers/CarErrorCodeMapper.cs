using AutoMapper;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CarErrorCodeMapper : BaseMapper<CarErrorCode, Domain.App.CarErrorCode>
    {
        public CarErrorCodeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}