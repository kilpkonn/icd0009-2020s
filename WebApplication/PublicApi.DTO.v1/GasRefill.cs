using System;
using PublicApi.DTO.v1.Base;
using AppUser = PublicApi.DTO.v1.Identity.AppUser;

namespace PublicApi.DTO.v1
{
    public class GasRefill : ApiDtoEntityId
    {
        public float? AmountRefilled { get; set; }
        public DateTime? Timestamp { get; set; }
        public float? Cost { get; set; }
        
        public Guid? AppUserId { get; set; }
        //public AppUser? AppUser { get; set; }
        
        public Guid? CarId { get; set; }
        //public Car? Car { get; set; }
    }
}