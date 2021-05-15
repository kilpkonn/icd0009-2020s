using System;
using System.Collections.Generic;
using Car.DAL.Base.Models;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class Car : DalEntity, IDalAppUserId
    {
        public Guid CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public IEnumerable<CarAccess>? CarAccesses { get; set; }
        public IEnumerable<CarErrorCode>? CarErrorCodes { get; set; }
    }
}