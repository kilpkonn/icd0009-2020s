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
    public class CarModelRepository : BaseRepository<CarModel, Domain.App.CarModel, AppDbContext>, ICarModelRepository
    {
        public CarModelRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarModelMapper(mapper))
        {
            
        }

        public override async Task<IEnumerable<CarModel>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateQuery(userId, tracking)
                .Include(x => x.CarMark)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations)
                .Select(e => Mapper.Map(e)!)
                .ToListAsync();
        }

        public override async Task<CarModel?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.CarMark)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public override CarModel Update(CarModel entity, Guid? userId)
        {
            var domainEntity = DbSet
                .AsNoTracking()
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations)
                .First(x => x.Id == entity.Id);

            var newEntity = Mapper.Map(entity)!;
            newEntity.Name = domainEntity.Name;
            newEntity.Name?.SetTranslation(entity.Name);
            
            var updatedEntity = DbSet.Update(newEntity!).Entity;
            return Mapper.Map(updatedEntity)!;
        }
    }
}