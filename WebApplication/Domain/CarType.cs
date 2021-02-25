using System;

namespace Domain
{
    public class CarType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        
        public Guid? CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }
    }
}