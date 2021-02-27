using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarAccessRepository: BaseRepository<CarAccess>, ICarAccessRepository
    {
        public CarAccessRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}