using System;
using PublicApi.DTO.v1.Base;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class Car : ApiDtoEntity
    {
        public Guid? CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        //public IEnumerable<CarAccess>? CarAccesses { get; set; }
        
    }
    
    public class NewCar
    {
        public Guid? CarTypeId { get; set; }
    }
}