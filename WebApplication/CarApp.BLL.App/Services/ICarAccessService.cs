using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarAccessService: IBaseEntityService<BllAppDTO.CarAccess, DAL.App.DTO.CarAccess>
    {
        
    }
}