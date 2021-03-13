using System;
using System.Collections.Generic;
using Car.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntity, IDomainAppUserId
    {
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
    }
}