using System;
using Car.Domain.Base;

namespace Domain.Base
{
    public class DomainEntityMeta: IDomainEntityMeta
    {
        public Guid CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}