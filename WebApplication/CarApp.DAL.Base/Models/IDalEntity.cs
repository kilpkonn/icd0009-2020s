using System;

namespace Car.DAL.Base.Models
{
    public interface IDalEntity : IDalEntity<Guid>
    {
    }

    public interface IDalEntity<TKey> : IDalEntityId<TKey>, IDalEntityMeta
        where TKey : IEquatable<TKey>
    {
    }
}