using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, DAL.App.DTO.Car>().ReverseMap();
            CreateMap<CarAccess, DAL.App.DTO.CarAccess>().ReverseMap();
            CreateMap<CarAccessType, DAL.App.DTO.CarAccessType>().ReverseMap();
            CreateMap<CarErrorCode, DAL.App.DTO.CarErrorCode>().ReverseMap();
            CreateMap<CarMark, DAL.App.DTO.CarMark>().ReverseMap();
            CreateMap<CarModel, DAL.App.DTO.CarModel>().ReverseMap();
            CreateMap<CarType, DAL.App.DTO.CarType>().ReverseMap();
            CreateMap<GasRefill, DAL.App.DTO.GasRefill>().ReverseMap();
            CreateMap<Track, DAL.App.DTO.Track>().ReverseMap();
            CreateMap<TrackLocation, DAL.App.DTO.TrackLocation>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }

}