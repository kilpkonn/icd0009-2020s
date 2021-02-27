using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarMark : DomainEntity
    {
        public string Name { get; set; } = null!;
    }
}