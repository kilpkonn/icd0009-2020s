using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarErrorCodeRepository: BaseRepository<CarErrorCode, AppDbContext>, ICarErrorCodeRepository
    {
        public CarErrorCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
        

        public override CarErrorCode Update(CarErrorCode entity, Guid? userId)
        {
            CheckAccess(entity, userId);
            return base.Update(entity, null);
        }

        public override CarErrorCode Remove(CarErrorCode entity, Guid? userId)
        {
            CheckAccess(entity, userId);
            return base.Remove(entity, null);
        }

        public override async Task<IEnumerable<CarErrorCode>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateQuery(null, true)
                .Include(e => e.Car)
                .Where(e => e.Car!.AppUserId == userId)
                .ToListAsync();
        }

        public override async Task<CarErrorCode?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            return await CreateQuery(null)
                .Include(e => e.Car)
                .Where(e => e.Car!.AppUserId == userId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task<bool> ExistsAsync(Guid id, Guid? userId)
        {
            CheckAccess(id, userId);
            return await base.ExistsAsync(id, null);
        }

        public override async Task<CarErrorCode> RemoveAsync(Guid id, Guid? userId)
        {
            CheckAccess(id, userId);
            return await base.RemoveAsync(id, null);
        }
        
        private void CheckAccess(CarErrorCode entity, Guid? userId)
        {
            var car = DbContext.Cars.FirstOrDefault(c => c.Id == entity.CarId);
            if (car == null || car.AppUserId != userId)
            {
                throw new UnauthorizedAccessException("Unauthorized car error code access.");
            }
        }
        
        private void CheckAccess(Guid entityId, Guid? userId)
        {
            var entity = DbContext.CarErrorCodes
                .Include(e => e.Car)
                .FirstOrDefault(e => e.Id == entityId);
            
            if (entity == null || entity.Car!.AppUserId != userId)
            {
                throw new UnauthorizedAccessException("Unauthorized car error code access.");
            }
        }
    }
}