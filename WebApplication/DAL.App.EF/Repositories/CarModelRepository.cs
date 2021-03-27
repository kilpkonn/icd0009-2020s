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
    public class CarModelRepository : BaseRepository<DTO.CarModel, Domain.App.CarModel, AppDbContext>, ICarModelRepository
    {
        public CarModelRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarModelMapper(mapper))
        {
            
        }

        public override async Task<IEnumerable<CarModel>> GetAllAsync(Guid? userId, bool tracking = false)
        {
            return await CreateQuery(userId, tracking)
                .Include(x => x.CarMark)
                .Select(e => Mapper.Map(e)!)
                .ToListAsync();
        }
    }
}