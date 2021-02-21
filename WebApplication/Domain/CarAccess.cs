using System;

namespace Domain
{
    public class CarAccess
    {
        public Guid Id { get; set; }
        
        public Guid? UserId { get; set; }
        // TODO: User link
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }
        
        public Guid? CarAccessTypeId { get; set; }
        public CarAccessType? CarAccessType { get; set; }
    }
}