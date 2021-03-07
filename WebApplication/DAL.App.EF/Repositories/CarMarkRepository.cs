using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.EF.Repositories
{
    public class CarMarkRepository: BaseRepository<CarMark, AppDbContext>, ICarMarkRepository
    {
        public CarMarkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}