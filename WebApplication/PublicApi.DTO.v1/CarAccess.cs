using System;
using PublicApi.DTO.v1.Base;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class CarAccess : ApiDtoEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        //public Car? Car { get; set; }
        
        public Guid? CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
    
    public class NewCarAccess
    {
        public Guid AppUserId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? CarAccessTypeId { get; set; }
    }
}