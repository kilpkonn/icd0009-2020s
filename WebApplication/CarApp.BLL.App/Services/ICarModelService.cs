using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarModelService: IBaseEntityService<BllAppDTO.CarModel, DAL.App.DTO.CarModel>
    {
        
    }
}