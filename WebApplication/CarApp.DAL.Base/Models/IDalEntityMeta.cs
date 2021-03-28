using System;

namespace Car.DAL.Base.Models
{
    public interface IDalEntityMeta
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        
        Guid UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}