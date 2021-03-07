using System.Threading.Tasks;

namespace Car.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}