using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarMarkService: IBaseEntityService<BllAppDTO.CarMark, DAL.App.DTO.CarMark>
    {
        
    }
}