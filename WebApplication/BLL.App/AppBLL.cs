using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using CarApp.BLL.App;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;

namespace BLL.App
{
    public class AppBll : BaseBll<IAppUnitOfWork>, IAppBll
    {
        protected IMapper Mapper;

        public AppBll(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public ICarService Cars => GetService<ICarService>(() => new CarService(Uow, Uow.Cars, Mapper));

        public ICarAccessService CarAccesses =>
            GetService<ICarAccessService>(() => new CarAccessService(Uow, Uow.CarAccesses, Mapper));

        public ICarAccessTypeService CarAccessTypes =>
            GetService<ICarAccessTypeService>(() => new CarAccessTypeServices(Uow, Uow.CarAccessTypes, Mapper));

        public ICarErrorCodeService CarErrorCodes =>
            GetService<ICarErrorCodeService>(() => new CarErrorCodeServices(Uow, Uow.CarErrorCodes, Mapper));

        public ICarMarkService CarMarks =>
            GetService<ICarMarkService>(() => new CarMarkService(Uow, Uow.CarMarks, Mapper));

        public ICarModelService CarModels =>
            GetService<ICarModelService>(() => new CarModelService(Uow, Uow.CarModels, Mapper));

        public ICarTypeService CarTypes =>
            GetService<ICarTypeService>(() => new CarTypeService(Uow, Uow.CarTypes, Mapper));

        public IGasRefillService GasRefills =>
            GetService<IGasRefillService>(() => new GasRefillService(Uow, Uow.GasRefills, Mapper));

        public ITrackService Tracks => GetService<ITrackService>(() => new TrackService(Uow, Uow.Tracks, Mapper));

        public ITrackLocationService TrackLocations =>
            GetService<ITrackLocationService>(() => new TrackLocationService(Uow, Uow.TrackLocations, Mapper));
    }
}