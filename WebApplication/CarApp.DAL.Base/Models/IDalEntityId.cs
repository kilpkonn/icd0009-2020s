using System;

namespace Car.DAL.Base.Models
{
    public interface IDalEntityId<TKey>
    where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}