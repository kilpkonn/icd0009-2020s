using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarAccessType : DomainEntityId
    {
        public string Name { get; set; } = null!;
        public int AccessLevel { get; set; }
    }
}