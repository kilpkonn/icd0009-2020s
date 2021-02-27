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
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}