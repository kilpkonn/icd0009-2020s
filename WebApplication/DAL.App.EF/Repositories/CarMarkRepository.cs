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
    public class CarMarkRepository: BaseRepository<CarMark, Domain.App.CarMark, AppDbContext>, ICarMarkRepository
    {
        public CarMarkRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarMarkMapper(mapper))
        {
            
        }

        public override async Task<IEnumerable<CarMark>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations);
            return await query.Select(e => Mapper.Map(e)!).ToListAsync();
        }

        public override async Task<CarMark?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}