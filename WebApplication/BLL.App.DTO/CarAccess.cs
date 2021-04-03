using System;
using BLL.App.DTO.Identity;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarAccess : BllEntity
    {
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid? CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}