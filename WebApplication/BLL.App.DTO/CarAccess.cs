using System;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class CarAccess : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
        
        public Guid CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}