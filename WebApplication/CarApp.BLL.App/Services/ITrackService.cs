using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ITrackService: IBaseEntityService<BllAppDTO.Track, DAL.App.DTO.Track>
    {
        
    }
}