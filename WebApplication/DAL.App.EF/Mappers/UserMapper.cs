using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.EF.Mappers
{
    public class UserMapper : BaseMapper<AppUser, Domain.App.Identity.AppUser>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}