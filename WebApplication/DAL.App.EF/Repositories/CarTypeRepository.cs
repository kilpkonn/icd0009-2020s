using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CarTypeRepository : BaseRepository<CarType, AppDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}