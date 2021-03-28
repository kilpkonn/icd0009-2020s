using System;

namespace CarApp.BLL.Base.Models
{
    public interface IBllEntityMeta
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        
        Guid UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}