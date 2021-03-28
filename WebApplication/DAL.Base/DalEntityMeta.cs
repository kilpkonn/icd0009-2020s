using System;
using Car.DAL.Base.Models;

namespace DAL.Base
{
    public class DalEntityMeta: IDalEntityMeta
    {
        public Guid CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}