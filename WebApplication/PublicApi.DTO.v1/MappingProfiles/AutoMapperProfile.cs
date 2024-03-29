using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, BLL.App.DTO.Car>().ReverseMap();
            CreateMap<CarAccess, BLL.App.DTO.CarAccess>().ReverseMap();
            CreateMap<CarAccessType, BLL.App.DTO.CarAccessType>().ReverseMap();
            CreateMap<CarErrorCode, BLL.App.DTO.CarErrorCode>().ReverseMap();
            CreateMap<CarMark, BLL.App.DTO.CarMark>().ReverseMap();
            CreateMap<CarModel, BLL.App.DTO.CarModel>().ReverseMap();
            CreateMap<CarType, BLL.App.DTO.CarType>().ReverseMap();
            CreateMap<GasRefill, BLL.App.DTO.GasRefill>().ReverseMap();
            CreateMap<Track, BLL.App.DTO.Track>().ReverseMap();
            CreateMap<TrackLocation, BLL.App.DTO.TrackLocation>().ReverseMap();
            CreateMap<AppUser, BLL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, BLL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Login, BLL.App.DTO.Identity.Login>().ReverseMap();
            CreateMap<Register, BLL.App.DTO.Identity.Register>().ReverseMap();
            CreateMap<JwtResponse, BLL.App.DTO.Identity.JwtResponse>().ReverseMap();
            
            CreateMap<NewCar, BLL.App.DTO.Car>().ReverseMap();
            CreateMap<NewCarAccess, BLL.App.DTO.CarAccess>().ReverseMap();
            CreateMap<NewCarAccessType, BLL.App.DTO.CarAccessType>().ReverseMap();
            CreateMap<NewCarErrorCode, BLL.App.DTO.CarErrorCode>().ReverseMap();
            CreateMap<NewCarMark, BLL.App.DTO.CarMark>().ReverseMap();
            CreateMap<NewCarModel, BLL.App.DTO.CarModel>().ReverseMap();
            CreateMap<NewCarType, BLL.App.DTO.CarType>().ReverseMap();
            CreateMap<NewGasRefill, BLL.App.DTO.GasRefill>().ReverseMap();
            CreateMap<NewTrack, BLL.App.DTO.Track>().ReverseMap();
            CreateMap<NewTrackLocation, BLL.App.DTO.TrackLocation>().ReverseMap();

        }
    }

}