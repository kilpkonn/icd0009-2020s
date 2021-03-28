using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarService: IBaseEntityService<BllAppDTO.Car, DAL.App.DTO.Car>
    {
        public Task<IEnumerable<BllAppDTO.Car>> GetAccessibleCarsForUser(Guid userId);
    }
}