using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarAccessTypeRepository: BaseRepository<CarAccessType, AppDbContext>, ICarAccessTypeRepository
    {
        public CarAccessTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CarAccessType> FindByNameAsync(string name)
        {
            return await DbSet.Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}