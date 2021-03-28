using System;
using BLL.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarErrorCode : BllEntity
    {
        public int CanId { get; set; }
        public long CanData { get; set; }
        
        public Guid CarId { get; set; }
        public Domain.App.Car? Car { get; set; }
    }
}