using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class GasRefillRepository: BaseRepository<GasRefill>, IGasRefillRepository
    {
        public GasRefillRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}