using System;

namespace CarApp.BLL.Base.Models
{
    public interface IBllEntityId<TKey>
    where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}