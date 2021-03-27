using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarMark : DomainEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}