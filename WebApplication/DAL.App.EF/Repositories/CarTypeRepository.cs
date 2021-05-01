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
    public class CarTypeRepository : BaseRepository<DTO.CarType, Domain.App.CarType, AppDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarTypeMapper(mapper))
        {
            
        }

        public override async Task<IEnumerable<DTO.CarType>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateQuery(userId, tracking)
                .Include(x => x.CarModel)
                .ThenInclude(x => x!.CarMark)
                .Select(e => Mapper.Map(e)!)
                .ToListAsync();
        }

        public override async Task<CarType?> FirstOrDefaultAsync(Guid id, Guid? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking)
                .Include(x => x.CarModel)
                .ThenInclude(x => x!.CarMark);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}