using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CarAccessTypeRepository: BaseRepository<CarAccessType, AppDbContext>, ICarAccessTypeRepository
    {
        public CarAccessTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}