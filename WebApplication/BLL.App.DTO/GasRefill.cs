using System;
using BLL.App.DTO.Identity;
using BLL.Base;
using CarApp.BLL.Base.Models;

namespace BLL.App.DTO
{
    public class GasRefill : BllEntityId, IBllAppUser<AppUser>
    {
        public float? AmountRefilled { get; set; }
        public DateTime? Timestamp { get; set; }
        public float? Cost { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
    }
}