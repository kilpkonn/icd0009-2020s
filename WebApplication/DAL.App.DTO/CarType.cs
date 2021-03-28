using System;
using DAL.Base;

namespace DAL.App.DTO
{
    public class CarType : DalEntityId
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}