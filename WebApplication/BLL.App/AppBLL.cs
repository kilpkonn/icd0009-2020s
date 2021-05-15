using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using CarApp.BLL.App;
using CarApp.BLL.App.Services;
using CarApp.DAL.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BLL.App
{
    public class AppBll : BaseBll<IAppUnitOfWork>, IAppBll
    {
        protected IMapper Mapper;
        protected SignInManager<AppUser> SignInManager;
        protected UserManager<AppUser> UserManager;
        protected ILoggerFactory LoggerFactory;
        protected IConfiguration Configuration;

        public AppBll(IAppUnitOfWork uow, IMapper mapper, SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, ILoggerFactory loggerFactory, IConfiguration configuration) : base(uow)
        {
            Mapper = mapper;
            SignInManager = signInManager;
            UserManager = userManager;
            LoggerFactory = loggerFactory;
            Configuration = configuration;
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

        public IAccountService Accounts =>
            GetService<IAccountService>(() => new AccountService(SignInManager, UserManager,
                new Logger<AccountService>(LoggerFactory), Configuration));

        public IUserService Users => GetService<IUserService>(() => new UserService(Uow, Mapper));
    }
}