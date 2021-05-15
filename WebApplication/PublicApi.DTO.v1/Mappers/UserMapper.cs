using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class UserMapper : BaseMapper<AppUser, BLL.App.DTO.Identity.AppUser>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}