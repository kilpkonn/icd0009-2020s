using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.EF.Repositories
{
    public class GasRefillRepository : BaseRepository<GasRefill, AppDbContext>, IGasRefillRepository
    {
        public GasRefillRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}