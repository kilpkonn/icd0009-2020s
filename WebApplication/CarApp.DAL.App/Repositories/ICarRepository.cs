using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Repositories;
using Model = DAL.App.DTO;

namespace CarApp.DAL.App.Repositories
{
    public interface ICarRepository: IBaseRepository<Model.Car>
    {
        public Task<IEnumerable<Model.Car>> GetAccessibleCarsForUser(Guid userId);
    }
}