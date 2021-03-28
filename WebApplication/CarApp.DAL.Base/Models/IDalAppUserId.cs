using System;

namespace Car.DAL.Base.Models
{
    public interface IDalAppUserId : IDalAppUserId<Guid>
    {
        
    }
    
    public interface IDalAppUserId<TKey>
    where TKey: IEquatable<TKey>
    {
        TKey AppUserId { get; set; }
    }
}