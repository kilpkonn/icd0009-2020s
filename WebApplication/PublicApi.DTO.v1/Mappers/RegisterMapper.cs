using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class RegisterMapper : BaseMapper<Register, BLL.App.DTO.Identity.Register>
    {
        public RegisterMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}