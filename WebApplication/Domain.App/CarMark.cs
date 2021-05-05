using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarMark : DomainEntity
    {
        public Guid NameId { get; set; }
        public LangString? Name { get; set; }

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}