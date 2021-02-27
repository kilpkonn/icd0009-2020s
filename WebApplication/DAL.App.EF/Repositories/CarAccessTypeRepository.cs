using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class CarAccessTypeRepository: BaseRepository<CarAccessType>, ICarAccessTypeRepository
    {
        public CarAccessTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}