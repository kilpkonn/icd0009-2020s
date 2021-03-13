using System;

namespace Car.Domain.Base
{
    public interface IDomainAppUserId : IDomainAppUserId<Guid>
    {
        
    }
    
    public interface IDomainAppUserId<TKey>
    where TKey: IEquatable<TKey>
    {
        TKey AppUserId { get; set; }
    }
}