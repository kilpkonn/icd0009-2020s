using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CarMarkRepository: BaseRepository<DTO.CarMark, Domain.App.CarMark, AppDbContext>, ICarMarkRepository
    {
        public CarMarkRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarMarkMapper(mapper))
        {
        }
    }
}