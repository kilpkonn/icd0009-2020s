using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarRepository : BaseRepository<DTO.Car, Domain.App.Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarMapper(mapper))
        {
        }

        public async Task<IEnumerable<DTO.Car>> GetAccessibleCarsForUser(Guid userId)
        {
            return (await DbContext.CarAccesses
                .AsNoTracking()
                .Include(x => x.Car)
                .ThenInclude(x => x!.CarType)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Car)
                .ThenInclude(x => x!.CarType)
                .ThenInclude(x => x!.CarModel)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Car)
                .ThenInclude(x => x!.CarType)
                .ThenInclude(x => x!.CarModel)
                .ThenInclude(x => x!.CarMark)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Car)
                .ThenInclude(x => x!.AppUser)
                .Where(x => x.AppUserId == userId)
                .Select(x => Mapper.Map(x.Car)!)
                .ToListAsync())!;
        }

        public override async Task<DTO.Car?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.CarType)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.CarType)
                .ThenInclude(x => x!.CarModel)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.CarType)
                .ThenInclude(x => x!.CarModel)
                .ThenInclude(x => x!.CarMark)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.AppUser)
                .Include(x => x.CarErrorCodes)
                .Include(x => x.CarAccesses)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.CarAccesses)
                .ThenInclude(x => x.CarAccessType)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations);

            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}