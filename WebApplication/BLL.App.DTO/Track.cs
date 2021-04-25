using System;
using System.ComponentModel.DataAnnotations;
using BLL.Base;
using Car.Domain.Base;
using CarApp.BLL.Base.Models;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class Track : BllEntity, IBllAppUser<AppUser>
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "StartTimestamp")]
        public DateTime? StartTimestamp { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "EndTimestamp")]
        public DateTime? EndTimestamp { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "Distance")]
        public float? Distance { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "CarId")]
        public Guid? CarId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "Car")]
        public Domain.App.Car? Car { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "AppUserId")]
        public Guid? AppUserId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Track), Name = "AppUser")]
        public AppUser? AppUser { get; set; }
    }
}