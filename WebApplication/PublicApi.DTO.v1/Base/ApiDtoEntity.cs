using System;

namespace PublicApi.DTO.v1.Base
{
    public class ApiDtoEntity : ApiDtoEntity<Guid>
    {
    }

    public class ApiDtoEntity<TKey> : ApiDtoEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}