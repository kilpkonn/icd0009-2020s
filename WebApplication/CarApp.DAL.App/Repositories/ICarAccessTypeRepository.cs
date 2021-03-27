using System.Threading.Tasks;
using Car.DAL.Base.Repositories;
using DAL.App.DTO;

namespace CarApp.DAL.App.Repositories
{
    public interface ICarAccessTypeRepository: IBaseRepository<CarAccessType>
    {
        public Task<CarAccessType> FindByNameAsync(string name);
    }
}