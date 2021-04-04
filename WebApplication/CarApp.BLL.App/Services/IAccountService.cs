using System.Threading.Tasks;
using BLL.App.DTO.Identity;

namespace CarApp.BLL.App.Services
{
    public interface IAccountService
    {
        
        Task<JwtResponse?> Register(Register register);
        Task<JwtResponse?> Login(Login login);
        
    }
}