using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Car : DomainEntityId
    {
        public Guid CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
    }
}