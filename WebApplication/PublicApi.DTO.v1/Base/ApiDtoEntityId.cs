using System;

namespace PublicApi.DTO.v1.Base
{
    public abstract class ApiDtoEntityId : ApiDtoEntityId<Guid>
    {
    }

    public abstract class ApiDtoEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}