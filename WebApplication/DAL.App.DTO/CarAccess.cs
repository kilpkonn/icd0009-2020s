using System;
using Car.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CarAccess : DomainEntityId, IDomainAppUser<AppUser>
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid CarId { get; set; }
        public DAL.App.DTO.Car? Car { get; set; }
        
        public Guid CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}