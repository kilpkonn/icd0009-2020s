using System;
using Car.DAL.Base.Models;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class CarAccess : DalEntity, IDalAppUser<AppUser>
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}