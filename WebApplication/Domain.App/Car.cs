using System;
using Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntity
    {
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}