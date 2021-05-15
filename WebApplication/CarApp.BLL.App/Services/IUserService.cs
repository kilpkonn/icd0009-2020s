using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;

namespace CarApp.BLL.App.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}