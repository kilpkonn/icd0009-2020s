using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarService: IBaseEntityService<BllAppDTO.Car, DAL.App.DTO.Car>
    {
        
    }
}