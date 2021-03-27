using System;
using Car.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Track : DomainEntity, IDomainAppUser<AppUser>
    {
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}