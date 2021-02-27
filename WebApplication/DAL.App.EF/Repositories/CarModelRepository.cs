using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarModelRepository: BaseRepository<CarModel>, ICarModelRepository
    {
        public CarModelRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}