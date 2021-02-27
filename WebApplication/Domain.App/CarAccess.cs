using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarAccess : DomainEntity
    {
        public Guid? UserId { get; set; }
        // TODO: User link
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid? CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}