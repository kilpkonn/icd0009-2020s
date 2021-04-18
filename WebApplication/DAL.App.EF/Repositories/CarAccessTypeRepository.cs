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
    public class CarAccessTypeRepository: BaseRepository<DTO.CarAccessType, Domain.App.CarAccessType, AppDbContext>, ICarAccessTypeRepository
    {
        public CarAccessTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CarAccessTypeMapper(mapper))
        {
        }

        public async Task<CarAccessType> FindByNameAsync(string name)
        {
            return await DbSet.AsNoTracking()
                .Where(x => x.Name == name)
                .Select(e => Mapper.Map(e)!)
                .FirstOrDefaultAsync();
        }
    }
}