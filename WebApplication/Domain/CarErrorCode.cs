using System;

namespace Domain
{
    public class CarErrorCode
    {
        public Guid Id { get; set; }
        
        public int CanId { get; set; }
        public long CanData { get; set; }
        
        public DateTime DetectedAt { get; set; }
    }
}