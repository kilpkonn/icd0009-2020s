using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarModel : DomainEntity
    {
        public Guid NameId { get; set; }
        public LangString? Name { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public Guid CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }

        public ICollection<CarType> CarTypes { get; set; } = new List<CarType>();
    }
}