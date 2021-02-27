using System;
using Car.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>
    {
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}