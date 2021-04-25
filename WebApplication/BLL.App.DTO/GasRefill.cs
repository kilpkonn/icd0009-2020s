using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using BLL.Base;
using CarApp.BLL.Base.Models;

namespace BLL.App.DTO
{
    public class GasRefill : BllEntityId, IBllAppUser<AppUser>
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "AmountRefilled")]
        public float? AmountRefilled { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "Timestamp")]
        public DateTime? Timestamp { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "Cost")]
        public float? Cost { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "AppUserId")]
        public Guid? AppUserId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "AppUser")]
        public AppUser? AppUser { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "CarId")]
        public Guid? CarId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.GasRefill), Name = "Car")]
        public Domain.App.Car? Car { get; set; }
    }
}