using System.Threading.Tasks;
using CarApp.BLL.Base.Services;
using BllAppDTO = BLL.App.DTO;

namespace CarApp.BLL.App.Services
{
    public interface ICarAccessTypeService: IBaseEntityService<BllAppDTO.CarAccessType, DAL.App.DTO.CarAccessType>
    {
        public Task<BllAppDTO.CarAccessType> FindByNameAsync(string name);

    }
}