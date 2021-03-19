using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarModel : DomainEntity
    {
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        
        public Guid CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }

        public ICollection<CarType> CarTypes { get; set; } = new List<CarType>();
    }
}