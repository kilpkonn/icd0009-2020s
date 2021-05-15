using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;

namespace BLL.App.Mappers
{
    public class UserMapper : BaseMapper<AppUser, DAL.App.DTO.Identity.AppUser>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}