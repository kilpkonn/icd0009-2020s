using System;
using Car.DAL.Base.Models;

namespace DAL.Base
{
    public abstract class DalEntityId : DalEntityId<Guid>
    {
    }

    public abstract class DalEntityId<TKey> : IDalEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}