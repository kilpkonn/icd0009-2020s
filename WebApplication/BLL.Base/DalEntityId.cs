using System;
using CarApp.BLL.Base.Models;

namespace BLL.Base
{
    public abstract class BllEntityId : BllEntityId<Guid>
    {
    }

    public abstract class BllEntityId<TKey> : IBllEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}