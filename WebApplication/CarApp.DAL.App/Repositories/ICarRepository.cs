using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Repositories;

namespace CarApp.DAL.App.Repositories
{
    public interface ICarRepository: IBaseRepository<Domain.App.Car>
    {
        public Task<IEnumerable<Domain.App.Car>> GetAccessibleCarsForUser(Guid userId);
    }
}