using System;
using Car.Domain.Base;

namespace Domain.Base
{
    public class DomainEntity : DomainEntity<Guid>
    {
    }

    public class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}