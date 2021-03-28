using System;

namespace CarApp.BLL.Base.Models
{
    public interface IBllEntity : IBllEntity<Guid>
    {
    }

    public interface IBllEntity<TKey> : IBllEntityId<TKey>, IBllEntityMeta
        where TKey : IEquatable<TKey>
    {
    }
}