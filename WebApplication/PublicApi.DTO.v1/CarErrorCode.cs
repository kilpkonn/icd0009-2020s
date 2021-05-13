using System;
using PublicApi.DTO.v1.Base;

namespace PublicApi.DTO.v1
{
    public class CarErrorCode : ApiDtoEntity
    {
        public int? CanId { get; set; }
        public long? CanData { get; set; }
        
        public Guid? CarId { get; set; }
        //public Car? Car { get; set; }
    }
    
    public class NewCarErrorCode
    {
        public int? CanId { get; set; }
        public long? CanData { get; set; }
        
        public Guid? CarId { get; set; }
    }
}