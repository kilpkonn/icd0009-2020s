using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;

namespace BLL.App.Services
{
    public class UserService: IUserService
    {
        protected UserMapper Mapper { get; set; }
        protected IAppUnitOfWork Uow { get; set; }
        public UserService(IAppUnitOfWork serviceUow, IMapper mapper)
        {
            Uow = serviceUow;
            Mapper = new UserMapper(mapper);
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return (await Uow.Users.GetAllAsync()).Select(x => Mapper.Map(x)!); 
        }
    }
}