using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.EF.Repositories
{
    public class CarErrorCodeRepository: BaseRepository<CarErrorCode, AppDbContext>, ICarErrorCodeRepository
    {
        public CarErrorCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}