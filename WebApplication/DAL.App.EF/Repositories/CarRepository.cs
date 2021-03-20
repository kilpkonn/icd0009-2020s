using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarRepository : BaseRepository<Domain.App.Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.App.Car>> GetAccessibleCarsForUser(Guid userId)
        {
            return (await DbContext.CarAccesses
                .Include(x => x.Car)
                .Where(x => x.AppUserId == userId)
                .Select(x => x.Car)
                .ToListAsync())!;

        }
    }
}