using System;
using CarApp.BLL.Base.Models;

namespace BLL.Base
{
    public class BllEntityMeta: IBllEntityMeta
    {
        public Guid CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}