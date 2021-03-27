using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarErrorCode : DomainEntity
    {
        public int CanId { get; set; }
        public long CanData { get; set; }
        
        public Guid CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
    }
}