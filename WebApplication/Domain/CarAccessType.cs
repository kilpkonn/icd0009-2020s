using System;

namespace Domain
{
    public class CarAccessType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int AccessLevel { get; set; }
    }
}