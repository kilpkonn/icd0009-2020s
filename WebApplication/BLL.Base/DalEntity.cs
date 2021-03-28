using System;
using CarApp.BLL.Base.Models;

namespace BLL.Base
{
    public class BllEntity : BllEntity<Guid>
    {
    }

    public class BllEntity<TKey> : BllEntityId<TKey>, IBllEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}