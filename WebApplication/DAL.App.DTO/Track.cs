using System;
using Car.DAL.Base.Models;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class Track : DalEntity, IDalAppUser<AppUser>
    {
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}