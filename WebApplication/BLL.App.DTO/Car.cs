using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using BLL.Base;

namespace BLL.App.DTO
{
    public class Car : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Car), Name = "CarTypeId")]
        public Guid? CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
    }
}