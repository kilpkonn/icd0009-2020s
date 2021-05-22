using System;

namespace Domain
{
    public abstract class DomainEntityId: DomainEntityId<Guid>
    {
    }

    public abstract class DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }

}