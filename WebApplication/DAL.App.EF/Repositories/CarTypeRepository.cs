using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarTypeRepository : BaseRepository<CarType, AppDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<IEnumerable<CarType>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateQuery(userId, tracking)
                .Include(x => x.CarModel)
                .ToListAsync();
        }
    }
}