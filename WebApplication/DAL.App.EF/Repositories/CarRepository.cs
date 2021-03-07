using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.EF.Repositories
{
    public class CarRepository : BaseRepository<Domain.App.Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}