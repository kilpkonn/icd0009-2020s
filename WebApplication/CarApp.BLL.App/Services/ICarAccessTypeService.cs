using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarAccessTypeService: IBaseEntityService<BllAppDTO.CarAccessType, DAL.App.DTO.CarAccessType>
    {
        
    }
}