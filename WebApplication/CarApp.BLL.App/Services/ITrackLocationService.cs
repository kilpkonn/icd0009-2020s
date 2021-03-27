using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ITrackLocationService: IBaseEntityService<BllAppDTO.TrackLocation, DAL.App.DTO.TrackLocation>
    {
        
    }
}