using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarErrorCodeRepository: BaseRepository<CarErrorCode>, ICarErrorCodeRepository
    {
        public CarErrorCodeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}