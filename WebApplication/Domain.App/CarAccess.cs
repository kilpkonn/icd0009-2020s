using System;
using Car.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class CarAccess : DomainEntity, IDomainAppUser<AppUser>
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}