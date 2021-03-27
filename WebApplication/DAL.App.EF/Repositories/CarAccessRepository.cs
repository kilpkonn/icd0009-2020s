using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class CarAccessRepository : BaseRepository<DTO.CarAccess, Domain.App.CarAccess, AppDbContext>,
        ICarAccessRepository
    {
        public CarAccessRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarAccessMapper(mapper))
        {
        }
    }
}