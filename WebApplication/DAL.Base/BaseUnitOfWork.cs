using System.Threading.Tasks;
using Car.DAL.Base;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork: IBaseUnitOfWork
    {
        public abstract Task<int> SaveChangesAsync();
    }
}