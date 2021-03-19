using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarType : DomainEntity
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}