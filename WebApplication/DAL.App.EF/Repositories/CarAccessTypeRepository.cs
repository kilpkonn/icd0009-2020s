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
    public class CarAccessTypeRepository: BaseRepository<CarAccessType, Domain.App.CarAccessType, AppDbContext>, ICarAccessTypeRepository
    {
        public CarAccessTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarAccessTypeMapper(mapper))
        {
        }

        public override async Task<IEnumerable<CarAccessType>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations);
            return await query.Select(e => Mapper.Map(e)!).ToListAsync();
        }

        public override async Task<CarAccessType?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations);;
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public async Task<CarAccessType> FindByNameAsync(string name)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations)
                .Where(x => x.Name == name)
                .Select(e => Mapper.Map(e)!)
                .FirstOrDefaultAsync();
        }
    }
}