using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarMark : DomainEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}