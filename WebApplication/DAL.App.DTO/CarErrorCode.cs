using System;
using DAL.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CarErrorCode : DalEntity
    {
        public int CanId { get; set; }
        public long CanData { get; set; }
        
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
    }
}