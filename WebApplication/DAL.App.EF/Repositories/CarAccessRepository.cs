using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CarAccessRepository: BaseRepository<CarAccess, AppDbContext>, ICarAccessRepository
    {
        public CarAccessRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}