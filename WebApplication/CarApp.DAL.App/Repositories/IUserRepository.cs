using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO.Identity;

namespace CarApp.DAL.App.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}