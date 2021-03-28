using System;
using Car.DAL.Base.Models;

namespace DAL.Base
{
    public class DalEntity : DalEntity<Guid>
    {
    }

    public class DalEntity<TKey> : DalEntityId<TKey>, IDalEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}