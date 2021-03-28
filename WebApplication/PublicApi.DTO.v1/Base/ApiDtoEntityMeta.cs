using System;

namespace PublicApi.DTO.v1.Base
{
    public class ApiDtoEntityMeta
    {
        public Guid CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}