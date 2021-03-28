using System;

namespace CarApp.BLL.Base.Models
{
    public interface IBllAppUserId : IBllAppUserId<Guid>
    {
        
    }
    
    public interface IBllAppUserId<TKey>
    where TKey: IEquatable<TKey>
    {
        TKey AppUserId { get; set; }
    }
}