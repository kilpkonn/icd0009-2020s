using System;
using Car.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class Track : DomainEntity, IDomainAppUser<AppUser>
    {
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}