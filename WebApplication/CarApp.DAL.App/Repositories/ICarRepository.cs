using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Repositories;

namespace CarApp.DAL.App.Repositories
{
    public interface ICarRepository: IBaseRepository<global::DAL.App.DTO.Car>
    {
        public Task<IEnumerable<global::DAL.App.DTO.Car>> GetAccessibleCarsForUser(Guid userId);
    }
}