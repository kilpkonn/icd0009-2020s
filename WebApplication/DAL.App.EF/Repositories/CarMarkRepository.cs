using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarMarkRepository: BaseRepository<CarMark>, ICarMarkRepository
    {
        public CarMarkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}