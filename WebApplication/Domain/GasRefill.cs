using System;

namespace Domain
{
    public class GasRefill
    {
        public Guid Id { get; set; }
        
        public float AmountRefilled { get; set; }
        public DateTime Timestamp { get; set; }
        public float Cost { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
    }
}