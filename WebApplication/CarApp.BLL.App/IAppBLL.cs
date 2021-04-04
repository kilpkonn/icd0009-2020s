using CarApp.BLL.App.Services;
using CarApp.BLL.Base;
using CarApp.BLL.Base.Services;

namespace CarApp.BLL.App
{
    public interface IAppBll : IBaseBll
    {
        ICarService Cars { get; }
        ICarAccessService CarAccesses { get; }
        ICarAccessTypeService CarAccessTypes { get; }
        ICarErrorCodeService CarErrorCodes { get; }
        ICarMarkService CarMarks { get; }
        ICarModelService CarModels { get; }
        ICarTypeService CarTypes { get; }
        IGasRefillService GasRefills { get; }
        ITrackService Tracks { get; }
        ITrackLocationService TrackLocations { get; }
        
        IAccountService Accounts { get; }
        
    }

}