using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class GasRefill : DomainEntityId
    {
        public float AmountRefilled { get; set; }
        public DateTime Timestamp { get; set; }
        public float Cost { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
    }
}