using System;
using Car.DAL.Base.Models;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class GasRefill : DalEntityId, IDalAppUser<AppUser>
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