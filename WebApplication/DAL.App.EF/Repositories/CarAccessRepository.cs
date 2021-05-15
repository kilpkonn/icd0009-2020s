using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarAccessRepository : BaseRepository<CarAccess, Domain.App.CarAccess, AppDbContext>,
        ICarAccessRepository
    {
        public CarAccessRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarAccessMapper(mapper))
        {
            
        }

        public override async Task<CarAccess?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.AppUser)
                .Include(x => x.CarAccessType)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public override async Task<IEnumerable<CarAccess>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.CarAccessType)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
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
                .Include(x => x.AppUser);
            return await query.Select(x => Mapper.Map(x)!).ToListAsync();
        }
    }
}