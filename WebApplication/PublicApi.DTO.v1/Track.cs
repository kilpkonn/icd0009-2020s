using System;
using PublicApi.DTO.v1.Base;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class Track : ApiDtoEntity //, IDomainAppUser<AppUser>
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