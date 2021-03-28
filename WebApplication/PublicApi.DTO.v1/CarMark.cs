using System.Collections.Generic;
using BLL.App.DTO;
using PublicApi.DTO.v1.Base;

namespace PublicApi.DTO.v1
{
    public class CarMark : ApiDtoEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}