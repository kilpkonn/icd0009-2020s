using System;

namespace Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        
        public DateTime AddedAt { get; set; }
        
        public Guid? CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}