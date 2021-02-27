using System;
using Car.Domain.Base;

namespace Domain.Base
{
    public class DomainEntityMeta: IDomainEntityMeta
    {
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}