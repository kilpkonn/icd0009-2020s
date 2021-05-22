using System;

namespace PublicApi.DTO.Base
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