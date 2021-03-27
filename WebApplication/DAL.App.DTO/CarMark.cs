using System.Collections.Generic;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CarMark : DomainEntityId
    {
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}