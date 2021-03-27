using System;
using Car.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GasRefill : DomainEntityId, IDomainAppUser<AppUser>
    {
        public float AmountRefilled { get; set; }
        public DateTime Timestamp { get; set; }
        public float Cost { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
    }
}