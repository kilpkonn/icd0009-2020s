using System;
using Car.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class GasRefill : DomainEntityId, IDomainAppUser<AppUser>
    {
        public float AmountRefilled { get; set; }
        public DateTime Timestamp { get; set; }
        public float Cost { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
    }
}