using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CarType : DomainEntityId
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}