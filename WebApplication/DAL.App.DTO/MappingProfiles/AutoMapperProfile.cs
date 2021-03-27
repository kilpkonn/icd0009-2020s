using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, Domain.App.Car>().ReverseMap();
            CreateMap<CarAccess, Domain.App.CarAccess>().ReverseMap();
            CreateMap<CarAccessType, Domain.App.CarAccessType>().ReverseMap();
            CreateMap<CarErrorCode, Domain.App.CarErrorCode>().ReverseMap();
            CreateMap<CarMark, Domain.App.CarMark>().ReverseMap();
            CreateMap<CarModel, Domain.App.CarModel>().ReverseMap();
            CreateMap<CarType, Domain.App.CarType>().ReverseMap();
            CreateMap<GasRefill, Domain.App.GasRefill>().ReverseMap();
            CreateMap<Track, Domain.App.Track>().ReverseMap();
            CreateMap<TrackLocation, Domain.App.TrackLocation>().ReverseMap();
            CreateMap<Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }

}