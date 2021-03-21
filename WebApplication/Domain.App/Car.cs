using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntity, IDomainAppUserId
    {
        public Guid CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
    }
}