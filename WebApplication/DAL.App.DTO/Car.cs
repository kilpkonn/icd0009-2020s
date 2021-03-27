using System;
using System.Collections.Generic;
using Car.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Car : DomainEntityId, IDomainAppUserId
    {
        public Guid CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
    }
}