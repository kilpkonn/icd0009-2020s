using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarTypeService: IBaseEntityService<BllAppDTO.CarType, DAL.App.DTO.CarType>
    {
        
    }
}