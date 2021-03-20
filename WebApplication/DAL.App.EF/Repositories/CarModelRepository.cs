using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CarModelRepository : BaseRepository<CarModel, AppDbContext>, ICarModelRepository
    {
        public CarModelRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}