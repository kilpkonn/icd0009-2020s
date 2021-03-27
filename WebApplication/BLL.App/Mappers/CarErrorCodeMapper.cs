using AutoMapper;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class CarErrorCodeMapper : BaseMapper<BLL.App.DTO.CarErrorCode, DAL.App.DTO.CarErrorCode>
    {
        public CarErrorCodeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}