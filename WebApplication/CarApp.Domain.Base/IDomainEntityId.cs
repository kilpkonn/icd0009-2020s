using System;

namespace Car.Domain.Base
{
    public interface IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}