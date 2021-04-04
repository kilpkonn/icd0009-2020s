using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class LoginMapper : BaseMapper<Login, BLL.App.DTO.Identity.Login>
    {
        public LoginMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}