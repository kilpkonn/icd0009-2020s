using System;
using System.Collections;
using Domain.Base;

namespace Domain.App
{
    public class CarErrorCode : DomainEntity
    {
        public int CanId { get; set; }
        public long CanData { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
    }
}