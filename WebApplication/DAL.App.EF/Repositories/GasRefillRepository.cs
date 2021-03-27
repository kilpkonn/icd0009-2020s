using AutoMapper;
using CarApp.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class GasRefillRepository : BaseRepository<DTO.GasRefill, Domain.App.GasRefill, AppDbContext>, IGasRefillRepository
    {
        public GasRefillRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GasRefillMapper(mapper))
        {
        }
    }
}