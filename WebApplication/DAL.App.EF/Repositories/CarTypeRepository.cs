using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarTypeRepository: BaseRepository<CarType>, ICarTypeRepository
    {
        public CarTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}