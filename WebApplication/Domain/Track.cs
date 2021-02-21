using System;

namespace Domain
{
    public class Track
    {
        public Guid Id { get; set; }
        
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public float Distance { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
    }
}