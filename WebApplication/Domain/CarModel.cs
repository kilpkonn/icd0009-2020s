using System;

namespace Domain
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        public Guid? CarTypeId { get; set; }
        public CarType? CarType { get; set; }
    }
}