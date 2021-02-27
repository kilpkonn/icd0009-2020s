using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Track : DomainEntity
    {
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
    }
}