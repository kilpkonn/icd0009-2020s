using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.DTO.Identity;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected UserMapper Mapper { get; set; }

        protected AppDbContext DbContext { get; set; }

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            Mapper = new UserMapper(mapper);
            DbContext = dbContext;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await DbContext.Users.AsNoTracking()
                .Select(x => Mapper.Map(x)!)
                .ToListAsync();
        }
    }
}