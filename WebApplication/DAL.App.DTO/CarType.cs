using System;
using DAL.Base;

namespace DAL.App.DTO
{
    public class CarType : DalEntity
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}