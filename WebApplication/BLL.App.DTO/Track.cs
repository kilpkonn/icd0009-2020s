using System;
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
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}