using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarModel : DomainEntity
    {
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        
        public Guid CarTypeId { get; set; }
        public CarType? CarType { get; set; }
    }
}