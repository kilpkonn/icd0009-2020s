using System;
using BLL.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarType : BllEntity
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public BLL.App.DTO.CarModel? CarModel { get; set; }
    }
}