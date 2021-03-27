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
                .Include(x => x.Car)
                .ThenInclude(x => x!.CarType)
                .Where(x => x.AppUserId == userId)
                .Select(x => Mapper.Map(x.Car)!)
                .ToListAsync())!;
        }
    }
}