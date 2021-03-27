using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarErrorCodeService: IBaseEntityService<BllAppDTO.CarErrorCode, DAL.App.DTO.CarErrorCode>
    {
        
    }
}