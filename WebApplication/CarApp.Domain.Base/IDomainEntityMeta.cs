using System;

namespace Car.Domain.Base
{
    public interface IDomainEntityMeta
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        
        Guid UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}