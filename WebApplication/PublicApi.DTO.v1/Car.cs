using System;
using System.Collections.Generic;
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

        public IEnumerable<CarAccess> CarAccesses { get; set; } = new List<CarAccess>();
        public IEnumerable<CarErrorCode> CarErrorCodes { get; set; } = new List<CarErrorCode>();
    }
    
    public class NewCar
    {
        public Guid? CarTypeId { get; set; }
    }
}