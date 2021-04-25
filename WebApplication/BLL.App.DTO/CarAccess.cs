using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarAccess : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "AppUserId")]
        public Guid? AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "AppUser")]
        public AppUser? AppUser { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "CarId")]
        public Guid? CarId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "Car")]
        public Car? Car { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "AccessTypeId")]
        public Guid? CarAccessTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccess), Name = "AccessType")]
        public CarAccessType? CarAccessType { get; set; }
    }
}