using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarType : DomainEntity
    {
        public string Name { get; set; } = null!;
        
        public Guid? CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }
    }
}